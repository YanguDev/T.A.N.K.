using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Projectile projectile;
    public Transform rifle;
    public Stats stats;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        stats.Initialize();
    }

    public void ShootProjectile(){
        GameObject newProjectile = Instantiate(projectile.gameObject, rifle.position, Quaternion.identity);
    }
}
