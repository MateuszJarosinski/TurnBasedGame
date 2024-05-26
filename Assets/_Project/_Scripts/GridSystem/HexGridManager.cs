using Core.Mouse3D;
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
        private GridObject _lastGridObject;

        private void Awake()
        {
            _hexGrid = new HexGrid<GridObject>(width, height, cellSize, originPos, () => new GridObject());

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject gObject = Instantiate(hexPrefab, _hexGrid.GetWorldPosition(x, z), Quaternion.identity);
                    _hexGrid.GetGridObject(x, z).Init(gObject);
                }
            }
        }

        private void Update()
        {
            HighlightHex();
        }

        private void HighlightHex()
        {
            if (_lastGridObject != null) _lastGridObject.GridHighlight.Deselect();

            _lastGridObject = _hexGrid.GetGridObject(Mouse3D.GetMouseWorldPosition());

            if (_lastGridObject != null) _lastGridObject.GridHighlight.Select();
        }
    }
}
