using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats stats;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        stats.Initialize();

        stats.onHealthChanged += HealthChanged;
    }

    private void Update(){
        transform.Translate(transform.forward * stats.moveSpeed * Time.deltaTime, Space.World);
    }

    private void HealthChanged(int health){
        if(health <= 0)
            Die();
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Line")){
            Die();
        }
    }
}
