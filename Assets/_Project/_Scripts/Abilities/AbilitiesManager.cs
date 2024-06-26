using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesManager : MonoBehaviour
    {
        public event Action<AbilityData> OnAbilityAdded;
        public event Action<AbilityData> OnAbilityRemoved;

        [SerializeField] private List<Ability> abilities;
        [SerializeField] private Transform abilitiesTransform;

        public AbilityData DEBUG_ablity;

        public void ExecuteAbility(AbilityType type)
        {
            foreach (var ability in abilities)
            {
                if (ability.Type == type)
                {
                    ability.Execute();
                    break;
                }
            }
        }

        public void AddAbility(AbilityData data)
        {
            GameObject abilityGameObject = Instantiate(data.AbilityGameObject, abilitiesTransform);
            abilities.Add(abilityGameObject.GetComponent<Ability>());

            OnAbilityAdded?.Invoke(data);
        }

        public void RemoveAbility(AbilityData data)
        {
            foreach (var ability in abilities)
            {
                if (ability.Type == data.Type)
                {
                    Destroy(ability.gameObject);
                    abilities.Remove(ability);
                    break;
                }
            }

            OnAbilityRemoved?.Invoke(data);
        }

        [Button]
        public void DEBUG_AddAbility()
        {
            AddAbility(DEBUG_ablity);
        }

        [Button]
        public void DEBUG_RemoveAbility()
        {
            RemoveAbility(DEBUG_ablity);
        }
    }
}
