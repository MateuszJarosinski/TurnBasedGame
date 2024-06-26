using Core.Mouse3D;
using GridSystem.Pathfinding;
using UnityEngine;

namespace GridSystem
{
    public class HexGridManager : MonoBehaviour
    {
        public PathfindingHex Pathfinding { get; private set; }
        public HexGrid<GridObject> HexGrid { get; private set; }

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPos;
        [SerializeField] private GameObject hexPrefab;

        private GridObject _lastHighlighted;
        private GridObject _lastSelected;

        private void Awake()
        {
            HexGrid = new HexGrid<GridObject>(width, height, cellSize, originPos, () => new GridObject());

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject gObject = Instantiate(hexPrefab, HexGrid.GetWorldPosition(x, z), Quaternion.identity);
                    HexGrid.GetGridObject(x, z).Init(gObject, x, z);
                }
            }

            Pathfinding = new PathfindingHex(HexGrid);
        }

        private void Update()
        {
            HighlightHex();

            if (Input.GetMouseButtonDown(1))
            {
                SelectHex();
            }
        }

        private void HighlightHex()
        {
            if (_lastHighlighted != null) _lastHighlighted.GridHighlight.HighlightOff();

            _lastHighlighted = HexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition());

            if (_lastHighlighted != null && _lastHighlighted.PathfindingNode.isWalkable) _lastHighlighted.GridHighlight.HighlightOn();
        }

        public void SelectHex()
        {
            if (_lastSelected != null) _lastSelected.Deselect();

            _lastSelected = HexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition());

            if (_lastSelected != null && _lastHighlighted.PathfindingNode.isWalkable) _lastSelected.Select();
        }

        public void DeselectHex()
        {
            if (_lastSelected != null) _lastSelected.Deselect();
        }
    }
}