using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform rifle;
    public TankStats stats;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        stats.Initialize();
    }

    public void ShootProjectile(){
        GameObject newProjectile = Instantiate(projectile.gameObject, rifle.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().stats = stats;
    }
}
