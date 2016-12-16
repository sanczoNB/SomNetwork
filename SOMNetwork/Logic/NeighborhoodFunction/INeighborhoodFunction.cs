using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMNetwork.Logic.NeighborhoodFunction
{
    public interface INeighborhoodFunction
    {
        double GiveValue(int distance, double radius);
    }
}
