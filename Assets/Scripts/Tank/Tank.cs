using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : Unit
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform rifle;
    public TankStats stats;
    public override Stats Stats { get { return stats; } }

    public void ShootProjectile(){
        GameObject newProjectile = Instantiate(projectile.gameObject, rifle.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().stats = stats;
    }
}
