using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeCubeEnemy : Enemy
{
    public override void Die()
    {
        base.Die();

        HealDamaged();
    }

    private void HealDamaged(){
        List<Enemy> enemies = ServiceLocator.Resolve<EnemySpawner>().SpawnedEnemies;
        foreach(Enemy enemy in enemies){
            if(enemy.stats.CurrentHealthPoints < enemy.stats.MaxHealthPoints/2){
                enemy.stats.HealFully();
            }
        }
    }
}
