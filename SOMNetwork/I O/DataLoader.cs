using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra;

namespace SOMNetwork.I_O
{
    public class DataLoader
    {
        private static string _pattern = @"<(\d+\.?\d*)\s*,\s*(\d+\.?\d*)>";

        public List<Matrix<double>> LoadCoordinates(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            return lines.Select(ParseToMatrix).Where(x => x != null).ToList();

        }

        public List<Matrix<double>> LoadResults(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            return lines.Select(ParseToMatrix).ToList();
        }

        public List<int> LoadOrder(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            return lines.Select(int.Parse).ToList();
        }

        private Matrix<double> ParseToMatrix(string line)
        {

            var regex = new Regex(_pattern, RegexOptions.IgnoreCase);

            var match = regex.Match(line);
            if (match.Success)
            {
                var tab = new double[1, 2];
                for (int i = 1; i <= 2; i++)
                {
                    var group = match.Groups[i].Value;
                    var coordinate = double.Parse(group, CultureInfo.InvariantCulture);
                    tab[0, i - 1] = coordinate;
                }
                return Matrix<double>.Build.DenseOfArray(tab);
            }
            return null;
        }
    }
}
