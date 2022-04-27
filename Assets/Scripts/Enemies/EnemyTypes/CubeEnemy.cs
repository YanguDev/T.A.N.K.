using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : Enemy
{
    protected override void ReachedLine()
    {
        base.ReachedLine();

        HealCubes();
    }

    private void HealCubes(){
        List<Enemy> enemies = ServiceLocator.Resolve<EnemySpawner>().GetEnemies();

        foreach(Enemy enemy in enemies){
            if(enemy.type == EnemyType.Cube || enemy.type == EnemyType.LargeCube){
                enemy.stats.HealPercent(0.1f);
            }
        }
    }
}
