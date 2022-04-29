using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Explosive Projectile")]
public class ExplosiveProjectileAbility : ProjectileAbility
{
    [SerializeField] private float radius;
    [SerializeField] private ParticleSystem explosionParticles;
    
    public override void Trigger(GameObject abilityObject)
    {
        Projectile projectile = abilityObject.GetComponent<Projectile>();
        Explode(projectile);
    }

    protected override Projectile InstantiateProjectile(Tank tank)
    {
        return Instantiate(projectilePrefab, tank.Rifle.position, Quaternion.identity);
    }

    protected override void AttachProjectileEvents(Projectile projectile, Tank tank) { }

    private void Explode(Projectile projectile){
        ShowExplosion(projectile.transform.position);
        KillEnemies(projectile.transform.position);
        Destroy(projectile.gameObject);
    }

    private void ShowExplosion(Vector3 position){
        ParticleSystem particles = Instantiate(explosionParticles, position, Quaternion.identity);
        var sz = particles.sizeOverLifetime;
        var size = sz.size;
        size.curveMultiplier = radius;
        sz.size = size;
    }

    private void KillEnemies(Vector3 position){
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach(Collider collider in colliders){
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null){
                enemy.stats.DealDamage(99);
            }
        }
    }
}
