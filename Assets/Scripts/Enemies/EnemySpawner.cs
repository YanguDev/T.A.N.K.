using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemiesList;
    public float spawnInterval;
    public float spawnPositionMargin;

    private void Start(){
        SpawnEnemy();
    }

    private void SpawnEnemy(){
        int index = Random.Range(0, enemiesList.Count);
        GameObject newEnemy = Instantiate(enemiesList[index].gameObject, GetRandomSpawnPosition(), transform.rotation);
        newEnemy.transform.SetParent(transform, true);
        RandomizeEnemyColor(newEnemy);

        Invoke("SpawnEnemy", spawnInterval);
    }

    private Vector3 GetRandomSpawnPosition(){
        float maxHorizontalPosition = transform.localScale.x/2 - spawnPositionMargin;
        float x = Random.Range(-maxHorizontalPosition, maxHorizontalPosition);
        Vector3 position = new Vector3(x, transform.position.y, transform.position.z);

        return position;
    }

    private void RandomizeEnemyColor(GameObject enemy){
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);

        enemy.GetComponent<MeshRenderer>().material.color = color;
    }
}
