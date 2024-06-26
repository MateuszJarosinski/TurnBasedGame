using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem.Pathfinding
{
    public class PathfindingMover : MonoBehaviour
    {
        [SerializeField] private bool _drawDebugPathLine;
        [SerializeField] private float moveSpeed = 1;

        private GridUnit _unit;
        private List<Vector3> _pathVectorList;
        private int pathIndex = -1;

        [ShowNonSerializedField] private bool _canMove;

        private void Awake()
        {
            _unit = GetComponent<GridUnit>();
        }

        public void Move(List<Vector3> pathVectorList)
        {
            if (!_canMove) return;

            _pathVectorList = pathVectorList;
            pathIndex = _pathVectorList.Count > 0 ? 0 : -1;

            //Debug line drawer
            if (_drawDebugPathLine)
            {
                for (int i = 0; i < _pathVectorList.Count - 1; i++)
                {
                    Debug.DrawLine(_pathVectorList[i], _pathVectorList[i + 1], Color.green, 3f);
                }
            }
        }

        private void Update()
        {
            PathfindingMove();
        }

        public bool CanMove(bool canMove) => _canMove = canMove;

        private void PathfindingMove()
        {
            if (pathIndex == -1) return;

            Vector3 nextPathPosition = _pathVectorList[pathIndex];

            transform.position = Vector3.MoveTowards(transform.position, nextPathPosition, Time.deltaTime * moveSpeed);

            if (transform.position == nextPathPosition)
            {
                pathIndex++;
                if (pathIndex >= _pathVectorList.Count)
                {
                    // End of path
                    pathIndex = -1;
                }
            }
        }
    }
}