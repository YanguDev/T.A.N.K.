using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Standard Projectile")]
public class ProjectileAbility : Ability
{
    public Projectile projectilePrefab;
    public Projectile ProjectilePrefab { get { return projectilePrefab; } }

    public override GameObject Use(GameObject tankObject)
    {
        Tank tank = tankObject.GetComponent<Tank>();
        return CreateProjectile(tank);
    }

    protected virtual GameObject CreateProjectile(Tank tank){
        Projectile projectile = InstantiateProjectile(tank);
        
        InitializeProjectile(projectile, tank);
        AttachProjectileEvents(projectile, tank);

        return projectile.gameObject;
    }

    protected virtual Projectile InstantiateProjectile(Tank tank){
        return tank.ProjectilesPool.GetObject(true);
    }

    protected void InitializeProjectile(Projectile projectile, Tank tank){
        projectile.transform.position = tank.Rifle.position;
        projectile.moveSpeed = tank.Stats.ProjectileSpeed;
        projectile.damage = tank.Stats.Damage;
    }

    protected virtual void AttachProjectileEvents(Projectile projectile, Tank tank){
        projectile.onDisappear += () => ReturnToPool(projectile, tank.ProjectilesPool);
    }

    private void ReturnToPool(Projectile projectile, Pooling<Projectile> pool){
        projectile.onDisappear -= () => ReturnToPool(projectile, pool);

        pool.StoreObject(projectile);
    }
}
