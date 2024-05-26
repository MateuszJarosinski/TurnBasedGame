using UnityEngine;

namespace GridSystem
{
    public class GridObject
    {
        public GridHighlight GridHighlight { get; private set; }

        public void Init(GameObject gameObject)
        {
            GridHighlight = gameObject.GetComponent<GridHighlight>();
        }
    }
}