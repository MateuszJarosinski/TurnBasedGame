namespace GridSystem.Pathfinding
{
    public class PathfindingHexNode
    {
        public int x;
        public int z;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable = true;

        public PathfindingHexNode cameFromNode;

        public PathfindingHexNode(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}
