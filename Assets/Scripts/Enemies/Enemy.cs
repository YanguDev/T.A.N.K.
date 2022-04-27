using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public EnemyType type;
    public int pointsReward;
    public int expReward;
    public Stats stats;
    protected override Stats Stats { get { return stats; } }

    private bool canMove = true;

    protected override void Initialize(){
        base.Initialize();

        ServiceLocator.Resolve<GameManager>().onGameEnd += () => canMove = false;
    }

    private void Update(){
        if(canMove)
            transform.Translate(transform.forward * stats.moveSpeed * Time.deltaTime, Space.World);
    }

    protected override void HealthChanged(int health)
    {
        base.HealthChanged(health);

        // Reward the tank since the enemy was killed by it
        if(health <= 0)
            Reward();
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }

    private void Reward(){
        ServiceLocator.Resolve<GameManager>().RewardTank(pointsReward, expReward);
    }

    protected virtual void ReachedLine() { }

    private void OnTriggerEnter(Collider collider){
        // When enemy reaches the red line
        if(collider.CompareTag("Line")){
            ServiceLocator.Resolve<GameManager>().DamageTank(1);
            Die();
            ReachedLine();
        }
    }
}

public enum EnemyType{
    Cube, LargeCube, Sphere, LargeSphere
}
