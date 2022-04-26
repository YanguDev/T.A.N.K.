using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : Unit
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private ExplosiveProjectile specialProjectile;
    [SerializeField] private Transform rifle;
    [SerializeField] private Image cooldownIndicator;
    public TankStats stats;
    protected override Stats Stats { get { return stats; } }

    private ExplosiveProjectile currentSpecialProjectile;
    private Coroutine specialCooldownCoroutine;

    private bool canShootSpecial = true;

    protected override void Initialize()
    {
        base.Initialize();

        currentSpecialProjectile = null;
        ResetSpecialCooldown();
    }

    public Projectile CreateProjectile(Projectile projectile){
        Projectile newProjectile = Instantiate(projectile, rifle.position, Quaternion.identity);
        newProjectile.moveSpeed = stats.projectileSpeed;
        newProjectile.damage = stats.damage;

        return newProjectile;
    }

    public void ShootProjectile(){
        CreateProjectile(projectile);
    }

    public void ShootSpecialProjectile(){
        if(currentSpecialProjectile != null){
            currentSpecialProjectile.Explode();
            return;
        }

        if(!canShootSpecial) return;

        currentSpecialProjectile = (ExplosiveProjectile)CreateProjectile(specialProjectile);
        
        specialCooldownCoroutine = StartCoroutine(SpecialCooldown());
    }

    private IEnumerator SpecialCooldown(){
        canShootSpecial = false;
        
        float t = 0;
        while(t < stats.specialCooldown){
            t += Time.deltaTime;
            cooldownIndicator.fillAmount = t/stats.specialCooldown;
            yield return null;
        }

        canShootSpecial = true;
    }

    private void ResetSpecialCooldown(){
        if(specialCooldownCoroutine != null)
            StopCoroutine(specialCooldownCoroutine);
        cooldownIndicator.fillAmount = 1;
        canShootSpecial = true;
    }
}
