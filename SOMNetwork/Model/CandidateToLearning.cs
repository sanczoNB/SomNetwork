using MathNet.Numerics.LinearAlgebra;

namespace SOMNetwork.Model
{
    public class CandidateToLearning
    {
        public Matrix<double> Weights { get; set; }

        public int DistanceFromBest { get; set; }
    }
}
