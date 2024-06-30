using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesManager : MonoBehaviour
    {
        public event Action OnCastSpell;

        public event Action<AbilityData> OnAbilityAdded;
        public event Action<AbilityData> OnAbilityRemoved;

        [SerializeField] private List<Ability> abilities;
        [SerializeField] private Transform abilitiesTransform;

        public AbilityData DEBUG_ablity;

        private bool _canCast;

        public void ExecuteAbility(AbilityType type)
        {
            if (!_canCast) return;

            foreach (var ability in abilities)
            {
                if (ability.Type == type)
                {
                    ability.Execute();
                    OnCastSpell?.Invoke();
                    break;
                }
            }
        }

        public void AddAbility(AbilityData data)
        {
            //check if this ability already exists
            foreach (var ability in abilities)
            {
                if (ability.Type == data.Type)
                {
                    Debug.Log("<color=orange>This ability already exists</color>");
                    return;
                }
            }

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

        public void CanCastSpell(bool canCast)
        {
            _canCast = canCast;
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
