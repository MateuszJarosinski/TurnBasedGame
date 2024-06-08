using System.Collections.Generic;
using UnityEngine;

namespace GridSystem.Pathfinding
{
    public class PathfindingHex
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private HexGrid<GridObject> _grid;

        private List<PathfindingHexNode> openList;
        private List<PathfindingHexNode> closedList;

        public PathfindingHex(HexGrid<GridObject> grid)
        {
            _grid = grid;
        }

        public GridObject GetNode(int x, int y)
        {
            return _grid.GetGridObject(x, y);
        }

        private List<PathfindingHexNode> GetNeighbourList(PathfindingHexNode currentNode)
        {
            List<PathfindingHexNode> neighbourList = new List<PathfindingHexNode>();

            bool oddRow = currentNode.z % 2 == 1;

            if (currentNode.x - 1 >= 0)
            {
                // Left
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z).PathfindingNode);
            }
            if (currentNode.x + 1 < _grid.GetWidth())
            {
                // Right
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z).PathfindingNode);
            }
            if (currentNode.z - 1 >= 0)
            {
                // Down
                neighbourList.Add(GetNode(currentNode.x, currentNode.z - 1).PathfindingNode);
            }
            if (currentNode.z + 1 < _grid.GetHeight())
            {
                // Up
                neighbourList.Add(GetNode(currentNode.x, currentNode.z + 1).PathfindingNode);
            }

            if (oddRow)
            {
                if (currentNode.z + 1 < _grid.GetHeight() && currentNode.x + 1 < _grid.GetWidth())
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z + 1).PathfindingNode);
                }
                if (currentNode.z - 1 >= 0 && currentNode.x + 1 < _grid.GetWidth())
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z - 1).PathfindingNode);
                }
            }
            else
            {
                if (currentNode.z + 1 < _grid.GetHeight() && currentNode.x - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z + 1).PathfindingNode);
                }
                if (currentNode.z - 1 >= 0 && currentNode.x - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z - 1).PathfindingNode);
                }
            }


            return neighbourList;
        }

        private List<PathfindingHexNode> CalculatePath(PathfindingHexNode endNode)
        {
            List<PathfindingHexNode> path = new List<PathfindingHexNode>();
            path.Add(endNode);
            PathfindingHexNode currentNode = endNode;
            while (currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }
            path.Reverse();
            return path;
        }

        private PathfindingHexNode GetLowestFCostNode(List<PathfindingHexNode> pathNodeList)
        {
            PathfindingHexNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }

        private int CalculateDistanceCost(PathfindingHexNode a, PathfindingHexNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.z - b.z);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_STRAIGHT_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        public List<PathfindingHexNode> FindPath(int startX, int startZ, int endX, int endZ)
        {
            PathfindingHexNode startNode = _grid.GetGridObject(startX, startZ).PathfindingNode;
            PathfindingHexNode endNode = _grid.GetGridObject(endX, endZ).PathfindingNode;

            if (startNode == null || endNode == null)
            {
                // Invalid Path
                return null;
            }

            openList = new List<PathfindingHexNode> { startNode };
            closedList = new List<PathfindingHexNode>();

            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                for (int y = 0; y < _grid.GetHeight(); y++)
                {
                    PathfindingHexNode pathNode = _grid.GetGridObject(x, y).PathfindingNode;
                    pathNode.gCost = 99999999;
                    pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathfindingHexNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode)
                {
                    // Reached final node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathfindingHexNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode)) continue;
                    if (!neighbourNode.isWalkable)
                    {
                        closedList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }

            // Out of nodes on the openList
            return null;
        }

        public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            _grid.GetXZ(startWorldPosition, out int startX, out int startY);
            _grid.GetXZ(endWorldPosition, out int endX, out int endY);

            List<PathfindingHexNode> path = FindPath(startX, startY, endX, endY);
            if (path == null)
            {
                return null;
            }
            else
            {
                List<Vector3> vectorPath = new List<Vector3>();
                foreach (PathfindingHexNode pathNode in path)
                {
                    vectorPath.Add(_grid.GetWorldPosition(pathNode.x, pathNode.z));
                }
                return vectorPath;
            }
        }
    }
}
