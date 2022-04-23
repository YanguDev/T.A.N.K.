using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    public int maxHealthPoints;
    public float moveSpeed;

    private int currentHealthPoints;

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    public void Initialize(){
        currentHealthPoints = maxHealthPoints;
    }

    public void ChangeHealth(int amount){
        currentHealthPoints += amount;

        if(onHealthChanged != null) onHealthChanged.Invoke(currentHealthPoints);
    }
}
