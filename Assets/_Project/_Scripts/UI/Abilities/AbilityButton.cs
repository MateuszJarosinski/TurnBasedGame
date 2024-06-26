using Abilities;
using TMPro;
using UnityEngine;

namespace UI.Abilities
{
    public class AbilityButton : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }

        public AbilityType Type { get; private set; }
        [SerializeField] private AbilitiesManager manager;
        [SerializeField] private TextMeshProUGUI abilityNameText;

        public void ExecuteAbility()
        {
            manager.ExecuteAbility(Type);
        }

        public void InitButton(string abilityName, AbilityType type)
        {
            Type = type;
            abilityNameText.text = abilityName;
            IsOccupied = true;
        }

        public void FreeButton()
        {
            Type = AbilityType.None;
            abilityNameText.text = "";
            IsOccupied = false;
        }
    }
}
