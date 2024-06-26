using Interfaces;
using UnityEngine;

namespace GridSystem
{
    public class UnitDetector : MonoBehaviour
    {
        private GridObject _gridObject;

        private IAmGridUnit _unit;

        public void Init(GridObject gridObject)
        {
            _gridObject = gridObject;
        }

        public void UnitEntered(GameObject gameObject)
        {
            _unit = gameObject.GetComponent<IAmGridUnit>();

            if (_unit != null) UnitDetected();
        }

        public void UnitExited(GameObject gameObject)
        {
            if (_unit == gameObject.GetComponent<IAmGridUnit>()) UnitMissed();
        }

        private void UnitDetected()
        {
            _gridObject.SetNewGridUnit(_unit);
        }

        private void UnitMissed()
        {
            _gridObject.SetNewGridUnit(null);
        }
    }
}
