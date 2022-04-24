using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesList;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnPositionMargin;

    private List<Enemy> spawnedEnemies = new List<Enemy>();

    private bool canSpawn;

    private void Start(){
        Initialize();
    }

    private void Initialize(){
        GameManager gm = ServiceLocator.Resolve<GameManager>();
        gm.onGameEnd += () => canSpawn = false;
        gm.onGameStart += StartSpawning;

        StartSpawning();
    }

    private void StartSpawning(){
        ClearEnemies();

        SpawnEnemy();
    }

    private void SpawnEnemy(){
        canSpawn = true;

        int index = Random.Range(0, enemiesList.Count);
        GameObject newEnemyObject = Instantiate(enemiesList[index].gameObject, GetRandomSpawnPosition(), transform.rotation);
        newEnemyObject.transform.SetParent(transform, true);
        RandomizeEnemyColor(newEnemyObject);

        Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
        newEnemy.onDeath += () => RemoveEnemy(newEnemy);
        spawnedEnemies.Add(newEnemy);
        StartCoroutine(SpawnCooldown());
    }

    private IEnumerator SpawnCooldown(){
        float t = 0;
        while(t < spawnInterval){
            t+= Time.deltaTime;
            if(!canSpawn)
                yield break;
            yield return null;
        }
        SpawnEnemy();
    }

    private void RemoveEnemy(Enemy enemy){
        spawnedEnemies.Remove(enemy);
    }

    private void ClearEnemies(){
        List<Enemy> tempList = new List<Enemy>();
        foreach(Enemy enemy in spawnedEnemies){
            tempList.Add(enemy);
        }

        foreach(Enemy enemy in tempList){
            enemy.Die();
        }
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
