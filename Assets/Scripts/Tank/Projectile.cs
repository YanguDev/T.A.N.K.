using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float maxDistance;
    [ReadOnly] public float moveSpeed;
    [ReadOnly] public int damage;
    private Vector3 initialPosition;

    public delegate void OnDisappear();
    public event OnDisappear onDisappear;

    private void Start(){
        initialPosition = transform.position;
    }

    private void Update(){
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, initialPosition) >= maxDistance)
            Disappear();
    }

    private void OnTriggerEnter(Collider collider){
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null){
            enemy.stats.DealDamage(damage);
            Disappear();
        }
    }

    private void Disappear(){
        if(onDisappear != null){
            onDisappear.Invoke();
        }else{
            Destroy(gameObject);
        }
    }
}
