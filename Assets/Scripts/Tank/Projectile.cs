using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float maxDistance;
    [ReadOnly] public float moveSpeed;
    [ReadOnly] public int damage;
    private Vector3 initialPosition;

    private void Start(){
        initialPosition = transform.position;
    }

    private void Update(){
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, initialPosition) >= maxDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider){
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null){
            enemy.stats.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}
