using System;

namespace SOMNetwork.Logic.NeighborhoodFunction
{
    class Guassian : INeighborhoodFunction
    {
        public double GiveValue(int distance, double radius)
        {
            var result = Math.Exp(-(Math.Pow(distance,2)/(2*Math.Pow(radius,2))));

            var sigma2 = 1.0;

            //var result = (1/(Math.Sqrt(sigma2*2*Math.PI))) * Math.Exp(-Math.Pow(distance,2)/(2*sigma2));

            return result;
        }
    }
}
