using Abilities;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Data/Ability")]
public class AbilityData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GameObject AbilityGameObject { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public AbilityType Type { get; private set; }
}
