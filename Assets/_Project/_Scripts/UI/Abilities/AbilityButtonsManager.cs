using Abilities;
using UI.Abilities;
using UnityEngine;

public class AbilityButtonsManager : MonoBehaviour
{
    [SerializeField] private AbilitiesManager abilitiesManager;
    [SerializeField] private AbilityButton[] buttons;

    private void OnEnable()
    {
        if (abilitiesManager != null)
        {
            abilitiesManager.OnAbilityAdded += AddAbilityButton;
            abilitiesManager.OnAbilityRemoved += RemoveAbilityButton;
        }
    }

    private void OnDisable()
    {
        if (abilitiesManager != null)
        {
            {
                abilitiesManager.OnAbilityAdded -= AddAbilityButton;
                abilitiesManager.OnAbilityRemoved -= RemoveAbilityButton;
            }
        }
    }

    public void AddAbilityButton(AbilityData data)
    {
        foreach (var button in buttons)
        {
            if (!button.IsOccupied)
            {
                button.InitButton(data.Name, data.Type);
                break;
            }
        }
    }

    public void RemoveAbilityButton(AbilityData data)
    {
        foreach (var button in buttons)
        {
            if (button.Type == data.Type)
            {
                button.FreeButton();
                break;
            }
        }
    }
}
