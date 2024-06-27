using GridSystem;
using UnityEngine;

namespace Core.EventHandlers
{
    public class ObstacleDetector : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }

        private GridObject _gridObject;

        private int _detectedNum;

        public void Init(GridObject gridObject)
        {
            _gridObject = gridObject;
        }

        public void ObstacleEntered()
        {
            _detectedNum++;

            ChangeGridState(true);
        }

        public void ObstacleExited()
        {
            _detectedNum--;

            if (_detectedNum == 0) ChangeGridState(false);
        }

        private void ChangeGridState(bool occupied)
        {
            IsOccupied = occupied;
            _gridObject.PathfindingNode.isWalkable = !occupied;
            _gridObject.GridHighlight.SetVisibility(!occupied);
        }
    }
}
