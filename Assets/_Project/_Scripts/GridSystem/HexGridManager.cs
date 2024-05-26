using Core.Mouse3D;
using GridSystem.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class HexGridManager : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPos;

        [SerializeField] private GameObject hexPrefab;

        private HexGrid<GridObject> _hexGrid;
        private PathfindingHex pathfinding;

        private GridObject _lastGridObject;

        private void Awake()
        {
            _hexGrid = new HexGrid<GridObject>(width, height, cellSize, originPos, () => new GridObject());

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject gObject = Instantiate(hexPrefab, _hexGrid.GetWorldPosition(x, z), Quaternion.identity);
                    _hexGrid.GetGridObject(x, z).Init(gObject, x, z);
                }
            }

            pathfinding = new PathfindingHex(_hexGrid);
        }

        private void Update()
        {
            HighlightHex();
            DEBUG_PathFinding();
        }

        private void HighlightHex()
        {
            if (_lastGridObject != null) _lastGridObject.GridHighlight.Deselect();

            _lastGridObject = _hexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition());

            if (_lastGridObject != null && _lastGridObject.PathfindingNode.isWalkable) _lastGridObject.GridHighlight.Select();
        }

        private void DEBUG_PathFinding()
        {
            if (Input.GetMouseButtonDown(0))
            {
                List<Vector3> pathList = pathfinding.FindPath(Vector3.zero, Mouse3D.GetMouseWorldPosition());

                if (pathList == null) return;

                for (int i = 0; i < pathList.Count - 1; i++)
                {
                    Debug.DrawLine(pathList[i], pathList[i + 1], Color.green, 3f);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                _hexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition()).PathfindingNode.SetIsWalkable(false);
                _hexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition()).GridHighlight.SetVisibility(false);
            }
        }
    }
}