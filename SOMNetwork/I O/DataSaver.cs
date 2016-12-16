using System.Collections.Generic;
using System.Configuration;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using SOMNetwork.Model;

namespace SOMNetwork.I_O
{
    public class DataSaver
    {
        public void SaveWeights(SOMNet network)
        {

            var totalPath = ConfigurationManager.AppSettings["Results"];

            using (var file = new StreamWriter(totalPath))
            {
                for (var i = 0; i < network.Count; i++)
                {
                    file.WriteLine("<{0} , {1}>",network.GetNuronWeights(i).At(0,0), network.GetNuronWeights(i).At(0,1));
                }
            }
        }

        public void SaveOrder(List<int> order)
        {

            var totalPath = ConfigurationManager.AppSettings["Order"];

            using (var file = new StreamWriter(totalPath))
            {
                foreach (var t in order)
                {
                    file.WriteLine("{0}", t);
                }
            }
        }

        public void SaveNormalizedInputs(List<Matrix<double>> noramlizedinputs)
        {
            var totalPath = ConfigurationManager.AppSettings["NormalizedInputs"];

            using (var file = new StreamWriter(totalPath))
            {
                file.WriteLine("Znoramalizowane wektory wejśćia");
                foreach (var m in noramlizedinputs)
                {
                    file.WriteLine("<{0} , {1}>", m.At(0,0), m.At(0,1));
                }
            }
        }

    }
}
