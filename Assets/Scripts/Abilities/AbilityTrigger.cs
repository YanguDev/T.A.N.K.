using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityTrigger
{
    [SerializeField] private Ability ability;
    [SerializeField] private AbilityCooldown cooldown;

    private GameObject abilityObject;

    public Ability Ability { get { return ability; } }
    public AbilityCooldown AbilityCooldown { get { return cooldown; } }

    public void Use(GameObject context){
        if(cooldown == null){
            abilityObject = ability.Use(context);
            return;
        }

        if(abilityObject != null){
            ability.Trigger(abilityObject);
        }else if(cooldown.CanUse){
            abilityObject = ability.Use(context);
            cooldown.StartCooldown();
        }
    }
}
