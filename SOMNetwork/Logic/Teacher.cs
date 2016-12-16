using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using SOMNetwork.Logic.NeighborhoodFunction;
using SOMNetwork.Model;

namespace SOMNetwork.Logic
{
    public class Teacher
    {
        private readonly SOMNet _network;

        private INeighborhoodFunction _neighborhoodFunction;

        private int kohonenSuggest = 100000;

        private int _rmax;

        private int _rmin = 1;

        public Teacher(SOMNet network, INeighborhoodFunction neighborhoodFunction, int rmax)
        {
            _network = network;
            _neighborhoodFunction = neighborhoodFunction;
            _rmax = rmax;
        }

        public void Learn(List<Matrix<double>> inputs)
        {
            var oldr = _rmax;

            var lambda = kohonenSuggest / Math.Log(_rmax);

            

            for (var i = 0; i < kohonenSuggest; i++)
            {
                
                var r = _rmax*Math.Pow((_rmin/(double)_rmax), i/(double)kohonenSuggest) - 1; //???? aktualizacja promienia
                //var r = _rmax * Math.Exp(-(i/lambda));


                if ((int)Math.Round(r) != oldr)
                {
                    oldr = (int) Math.Round(r);
                    Console.WriteLine("Nowa wartość promienia {0} w iteracji {1}", oldr, i);
                }

                var index = i % inputs.Count;
                LarningOnSingleInput(inputs[index], r);

            }
        }

        public void LarningOnSingleInput(Matrix<double> input, double radius)
        {
            var bestIndex = _network.GetIndexOfTheMostMatchesNeuron(input);
            var candidateToLearning = _network.GetCandidatesToLearning(bestIndex, radius);

            candidateToLearning.ForEach(x => TeachCandidate(x, input, radius));
        }

        private void TeachCandidate(CandidateToLearning candidate, Matrix<double> input,double radius)
        {
            var weights = candidate.Weights;
            var distance = candidate.DistanceFromBest;

            var neighborhoodFactor = _neighborhoodFunction.GiveValue(distance, radius);

            var afterLearning = weights.Add(neighborhoodFactor*(input - weights));
            //Update weights
            weights.SetSubMatrix(0,0,afterLearning);
        }
    }
}
