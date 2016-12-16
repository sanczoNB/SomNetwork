using MathNet.Numerics.LinearAlgebra;

namespace SOMNetwork.Model
{
    public class Orderliness
    {
        public int Order { get; set; }

        public Matrix<double> Coordinate { get; set; }

        public Orderliness(int order, Matrix<double> coordinate)
        {
            Order = order;
            Coordinate = coordinate;
        }
    }
}
