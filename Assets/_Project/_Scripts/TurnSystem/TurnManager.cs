using Core.Singleton;
using GridSystem;
using Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace TurnSystem
{
    public class TurnManager : MonoBehaviourSingleton<TurnManager>
    {
        [SerializeField] private HexGridManager gridManager;

        private int _currentUnitIndex;

        [Button]
        public void StartTurnSystem()
        {
            _currentUnitIndex = 0;
            SpawnUnits();
            SelectUnit(_currentUnitIndex);
        }

        private void SpawnUnits()
        {
            gridManager.SpawnUnits();
        }

        private void SelectUnit(int index)
        {
            IAmGridUnit currentUnit = gridManager.Units[index];

            gridManager.Units[GetPreviousIndex()].MakeNonInteractable();
            currentUnit.MakeInteractable();
            gridManager.SelectHex(currentUnit.Position);
        }

        private void SetNextIndex()
        {
            if (_currentUnitIndex < gridManager.Units.Count - 1) _currentUnitIndex++;
            else _currentUnitIndex = 0;
        }

        private int GetPreviousIndex()
        {
            if (_currentUnitIndex == 0) return gridManager.Units.Count - 1;
            else return _currentUnitIndex - 1;
        }

        public void NextTurn()
        {
            SetNextIndex();
            SelectUnit(_currentUnitIndex);
        }

        [Button]
        private void DEBUG_SelectUnit()
        {
            SelectUnit(_currentUnitIndex);
        }
    }
}
