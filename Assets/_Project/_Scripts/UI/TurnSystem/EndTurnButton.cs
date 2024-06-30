using TurnSystem;
using UnityEngine;

namespace UI.TurnSystem
{
    public class EndTurnButton : MonoBehaviour
    {
        public void EndTurn()
        {
            TurnManager.Instance.NextTurn();
        }
    }
}
