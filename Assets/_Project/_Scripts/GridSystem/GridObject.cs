using Core.EventHandlers;
using GridSystem.Pathfinding;
using UnityEngine;

namespace GridSystem
{
    public class GridObject
    {
        public GridHighlight GridHighlight { get; private set; }
        public PathfindingHexNode PathfindingNode { get; private set; }
        public ObstacleDetector ObstacleDetector { get; private set; }

        public void Init(GameObject gameObject, int x, int z)
        {
            GridHighlight = gameObject.GetComponent<GridHighlight>();
            ObstacleDetector = gameObject.GetComponent<ObstacleDetector>();
            ObstacleDetector.Init(this);

            PathfindingNode = new PathfindingHexNode(x, z);
        }
    }
}