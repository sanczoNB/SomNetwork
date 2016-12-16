using System;
using MathNet.Numerics.LinearAlgebra;

namespace SOMNetwork.Helpers
{
    public class MatrixHelpers
    {
        public Matrix<double> Normalize(Matrix<double> m)
        {
            if (Math.Min(m.RowCount, m.ColumnCount) != 1)
            {
                return null;
            }

            var qudratic = m.Map(x => Math.Pow(x, 2));
            Matrix<double> ones;
            double length = 0;
            if (m.RowCount == 1)
            {
                ones = Matrix<double>.Build.Dense(m.ColumnCount, 1, Matrix<double>.One);
                length = Math.Sqrt(qudratic.Multiply(ones).At(0, 0));
            }
            else
            {
                ones = Matrix<double>.Build.Dense(1, m.RowCount);
                length = Math.Sqrt(ones.Multiply(qudratic).At(0,0));
            }

            return m.Map(x => x/length);
        }
    }
}
