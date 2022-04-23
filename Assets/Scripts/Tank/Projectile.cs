using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxDistance;
    public Stats stats;

    private Vector3 initialPosition;

    private void Start(){
        initialPosition = transform.position;
    }

    private void Update(){
        transform.Translate(transform.forward * stats.moveSpeed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, initialPosition) >= maxDistance)
            Destroy(gameObject);
    }
}
