using Abilities;
using UnityEngine;

public class HelloAbility : Ability
{
    public override void Execute()
    {
        Debug.Log("<color=yellow>Hello!</color>");
    }
}
