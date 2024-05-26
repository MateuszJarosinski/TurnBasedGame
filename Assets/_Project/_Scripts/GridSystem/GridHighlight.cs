using UnityEngine;

namespace GridSystem
{
    public class GridHighlight : MonoBehaviour
    {
        [SerializeField] private GameObject selectedVisual;

        public void Select()
        {
            selectedVisual.SetActive(true);
        }

        public void Deselect()
        {
            selectedVisual.SetActive(false);
        }
    }
}
