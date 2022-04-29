using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit<TStats> : MonoBehaviour
{
    public abstract TStats Stats { get; }

    public delegate void OnDeath();
    public event OnDeath onDeath;

    private void Awake(){
        Initialize();
    }

    protected virtual void Initialize(){
        Stats s = Stats as Stats;
        s.Initialize();
        s.onHealthChanged += HealthChanged;
    }

    protected virtual void HealthChanged(int health){
        if(health <= 0){
            Die();
        }
    }

    public virtual void Die(){
        if(onDeath != null) onDeath.Invoke();
    }
}
