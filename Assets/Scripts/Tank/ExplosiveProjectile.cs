using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    public float radius;
    public ParticleSystem explosionParticles;
    public void Explode(){
        ShowExplosion();
        DestroyEnemies();
        

        Destroy(gameObject);
    }

    private void ShowExplosion(){
        ParticleSystem particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        var sz = particles.sizeOverLifetime;
        var size = sz.size;
        size.curveMultiplier = radius;
        sz.size = size;
    }

    private void DestroyEnemies(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider collider in colliders){
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null){
                enemy.stats.Damage(99);
            }
        }
    }
}
