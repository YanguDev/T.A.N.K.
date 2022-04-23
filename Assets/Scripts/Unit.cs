using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private bool isDead;
    public abstract Stats Stats { get; }

    private void Awake(){
        Initialize();
    }

    protected virtual void Initialize(){
        Stats.Initialize();
        Stats.onHealthChanged += HealthChanged;
    }

    protected virtual void HealthChanged(int health){
        if(health <= 0){
            Die();
        }
    }

    protected virtual void Die(){
        isDead = true;
        Destroy(gameObject);
    }
}
