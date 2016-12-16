using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MoreLinq;

namespace SOMNetwork.Model
{
    public class SOMNet
    {
        private readonly List<Matrix<double>> _neurons;

        private readonly Random _random = new Random();

        public int Count => _neurons.Count;

        public SOMNet(double maxFirstWeightValue, double maxSecondWeightValue, int size)
        {
            _neurons = new List<Matrix<double>>();

            for (var i = 0; i < size; i++)
            {
                var firstWeight = _random.NextDouble()*maxFirstWeightValue;
                var secondWeight = _random.NextDouble()*maxSecondWeightValue;
                var array = new double[1,2];
                array[0, 0] = firstWeight;
                array[0, 1] = secondWeight;

                _neurons.Add(Matrix<double>.Build.DenseOfArray(array));
            }
        }

        public int GetNetworkSize()
        {
            return _neurons.Count;
        }

        public int GetIndexOfTheMostMatchesNeuron(Matrix<double> x)
        {
           var index = _neurons.Select(weights => weights.Subtract(x).L2Norm()).Index().MinBy(l => l.Value).Key;

            return index;
        }

        public List<CandidateToLearning> GetCandidatesToLearning(int bestIndex,double radiusD)
        {
            var radius = (int) Math.Round(radiusD);

            var result = new List<CandidateToLearning>();

            var best = _neurons[bestIndex];

            result.Add(new CandidateToLearning() {DistanceFromBest = 0, Weights = best});

            var distance = 1;

            for (var i = bestIndex + 1; distance <= radius; i++)
            {
                if (i == _neurons.Count)
                {
                    i = 0;
                }
                result.Add(new CandidateToLearning() {DistanceFromBest = distance, Weights = _neurons[i]});
                distance++;
            }

            distance = 1;

            for (var i = bestIndex - 1; distance <= radius; i--)
            {
                if (i == -1)
                {
                    i = _neurons.Count - 1;
                }
                result.Add(new CandidateToLearning() {DistanceFromBest = distance, Weights = _neurons[i]});
                distance++;
            }

            return result;
        }

        public Matrix<double> GetNuronWeights(int index)
        {
            return _neurons[index];
        }
    }
}
