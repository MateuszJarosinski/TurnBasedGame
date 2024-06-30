using Abilities;
using GridSystem.Pathfinding;
using UnityEngine;

namespace TurnSystem
{
    public class TurnObject : MonoBehaviour
    {
        [SerializeField] private int spellsToCast;

        private AbilitiesManager _abilitiesManager;
        private PathfindingMover _pathfindingMover;

        private int _castedSpellsCounter;

        private void Awake()
        {
            _abilitiesManager = GetComponent<AbilitiesManager>();
            _pathfindingMover = GetComponent<PathfindingMover>();
        }

        private void OnEnable()
        {
            _abilitiesManager.OnCastSpell += SpellCasted;
            _pathfindingMover.OnEndMove += MoveEnd;
        }

        private void OnDisable()
        {
            _abilitiesManager.OnCastSpell -= SpellCasted;
            _pathfindingMover.OnEndMove -= MoveEnd;
        }

        public void MakeTurn()
        {
            _castedSpellsCounter = 0;
            _abilitiesManager.CanCastSpell(true);
            _pathfindingMover.CanMove(true);
            _pathfindingMover.ResetSteps();
        }

        private void SpellCasted()
        {
            _castedSpellsCounter++;
            if (_castedSpellsCounter >= spellsToCast) _abilitiesManager.CanCastSpell(false);
        }

        private void MoveEnd()
        {
            _pathfindingMover.CanMove(false);
        }
    }
}
