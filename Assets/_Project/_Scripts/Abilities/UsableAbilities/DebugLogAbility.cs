using UnityEngine;

namespace Abilities
{
    public class DebugLogAbility : Ability
    {
        public override void Execute()
        {
            Debug.Log("<color=blue>Used Debug Log Ability!</color>");
        }
    }

}