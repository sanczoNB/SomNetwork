namespace SOMNetwork.Logic.NeighborhoodFunction
{
    public class SimpleFunctionOfNeighborhood : INeighborhoodFunction
    {
        public double GiveValue(int distance, double radius)
        {
            return distance <= radius ? 1 : 0;
        }
    }
}
