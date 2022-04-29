using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : Unit<TankStats>
{
    [Header("Objects")]
    [SerializeField] private Transform rifle;

    [Header("Abilities")]
    [SerializeField] private AbilityTrigger standardProjectile;
    [SerializeField] private AbilityTrigger specialProjectile;

    [Space]
    [SerializeField] private TankStats stats;

    private Pooling<Projectile> projectilesPool;
    
    public Transform Rifle { get { return rifle; } }
    public override TankStats Stats { get { return stats; } }
    public Pooling<Projectile> ProjectilesPool { get { return projectilesPool; } }


    protected override void Initialize()
    {
        base.Initialize();
        
        // Connect Stats' cooldown to the special ability
        specialProjectile.AbilityCooldown.Cooldown = stats.SpecialCooldown;
        stats.onCooldownChanged += (float cooldown) => specialProjectile.AbilityCooldown.Cooldown = cooldown;

        projectilesPool = new Pooling<Projectile>(50, (standardProjectile.Ability as ProjectileAbility).projectilePrefab);
    }

    public void ShootProjectile(){
        standardProjectile.Use(gameObject);
    }

    public void ShootSpecialProjectile(){
        specialProjectile.Use(gameObject);
    }
}
