using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : Enemy
{
    protected override void HealthChanged(int health)
    {
        base.HealthChanged(health);

        SpeedBuff();
    }

    private void SpeedBuff(){
        List<Enemy> enemies = ServiceLocator.Resolve<EnemySpawner>().GetEnemies();
        foreach(Enemy enemy in enemies){
            if(enemy.type == EnemyType.Sphere){
                enemy.stats.ChangeSpeed(0.1f);
            }
        }
    }
}
