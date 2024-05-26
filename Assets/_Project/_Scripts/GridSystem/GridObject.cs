using GridSystem.Pathfinding;
using UnityEngine;

namespace GridSystem
{
    public class GridObject
    {
        public GridHighlight GridHighlight { get; private set; }
        public PathfindingHexNode PathfindingNode { get; private set; }

        public void Init(GameObject gameObject, int x, int z)
        {
            GridHighlight = gameObject.GetComponent<GridHighlight>();
            PathfindingNode = new PathfindingHexNode(x, z);
        }
    }
}