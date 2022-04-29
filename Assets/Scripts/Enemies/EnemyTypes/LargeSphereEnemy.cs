using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeSphereEnemy : Enemy
{
    protected override void HealthChanged(int health)
    {
        base.HealthChanged(health);

        SpeedDebuff();
    }

    private void SpeedDebuff(){
        List<Enemy> enemies = ServiceLocator.Resolve<EnemySpawner>().SpawnedEnemies;
        foreach(Enemy enemy in enemies){
            if(enemy.type == EnemyType.Sphere || enemy.type == EnemyType.LargeSphere){
                enemy.stats.ChangeSpeed(-0.1f);
            }
        }
    }
}
