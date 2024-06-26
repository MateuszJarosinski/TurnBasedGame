using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [field: SerializeField] public AbilityType Type { get; private set; }
        public abstract void Execute();
    }
}
