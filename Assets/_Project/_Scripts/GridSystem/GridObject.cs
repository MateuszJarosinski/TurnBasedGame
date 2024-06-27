using Core.EventHandlers;
using GridSystem.Pathfinding;
using Interfaces;
using UnityEngine;

namespace GridSystem
{
    public class GridObject
    {
        public GridHighlight GridHighlight { get; private set; }
        public PathfindingHexNode PathfindingNode { get; private set; }
        public ObstacleDetector ObstacleDetector { get; private set; }
        public UnitDetector UnitDetector { get; private set; }
        public IAmGridUnit GridUnit { get; private set; }

        public void Init(GameObject gameObject, int x, int z)
        {
            GridHighlight = gameObject.GetComponent<GridHighlight>();
            ObstacleDetector = gameObject.GetComponent<ObstacleDetector>();
            ObstacleDetector.Init(this);
            UnitDetector = gameObject.GetComponent<UnitDetector>();
            UnitDetector.Init(this);

            PathfindingNode = new PathfindingHexNode(x, z);
        }

        public void Select()
        {
            GridHighlight.Select();

            if (GridUnit != null)
            {
                GridUnit.MakeInteractable();
                Debug.Log($"<color=green>There is unit on this hex</color>");
            }
            else
            {
                Debug.Log($"<color=orange>There is not unit on this hex</color>");
            }
        }

        public void Deselect()
        {
            GridHighlight.Deselect();

            if (GridUnit != null) GridUnit.MakeNonInteractable();
        }

        public void SetNewGridUnit(IAmGridUnit unit)
        {
            GridUnit = unit;
        }

        public bool IsOccupied()
        {
            if (ObstacleDetector.IsOccupied || GridUnit != null) return true;
            else return false;
        }
    }
}