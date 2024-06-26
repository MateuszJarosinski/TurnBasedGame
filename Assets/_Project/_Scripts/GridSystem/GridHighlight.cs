using UnityEngine;

namespace GridSystem
{
    public class GridHighlight : MonoBehaviour
    {
        [SerializeField] private GameObject defaultVisual;
        [SerializeField] private GameObject highlightedVisual;
        [SerializeField] private GameObject selectedVisual;

        public void HighlightOn()
        {
            if (!selectedVisual.activeSelf)
                highlightedVisual.SetActive(true);
        }

        public void HighlightOff()
        {
            highlightedVisual.SetActive(false);
        }

        public void Select()
        {
            selectedVisual.SetActive(true);
        }

        public void Deselect()
        {
            selectedVisual.SetActive(false);
        }

        public void SetVisibility(bool visible)
        {
            defaultVisual.SetActive(visible);
        }
    }
}
