using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public int pointsReward;
    public int expReward;
    public Stats stats;
    public override Stats Stats { get { return stats; } }

    private void Update(){
        transform.Translate(transform.forward * stats.moveSpeed * Time.deltaTime, Space.World);
    }

    protected override void HealthChanged(int health)
    {
        base.HealthChanged(health);

        if(health <= 0)
            RewardScore();
    }

    private void RewardScore(){
        ServiceLocator.Resolve<GameManager>().AddScore(points);
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Line")){
            GameManager gm = ServiceLocator.Resolve<GameManager>();
            gm.tank.stats.ChangeHealth(-1);
            Die();
        }
    }
}
