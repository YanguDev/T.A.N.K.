using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] private string abilityName;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;

    public abstract GameObject Use(GameObject context);
    public virtual void Trigger(GameObject abilityObject) { }
}
