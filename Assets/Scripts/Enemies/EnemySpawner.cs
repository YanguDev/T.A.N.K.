using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ChancePicker<Enemy> enemiesChancePicker;
    [SerializeField] private Transform floor;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnPositionMargin;
    [SerializeField][ReadOnly] private bool canSpawn;

    private List<Enemy> spawnedEnemies = new List<Enemy>();
    public List<Enemy> SpawnedEnemies { get { return spawnedEnemies; } }

    
    private void Start(){
        Initialize();
    }

    private void Initialize(){
        ServiceLocator.Register<EnemySpawner>(this);

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

        Enemy newEnemy = Instantiate(enemiesChancePicker.Roll(), GetRandomSpawnPosition(), transform.rotation);

        newEnemy.transform.SetParent(transform, true);
        newEnemy.transform.StandOnFloor(floor);
        newEnemy.SetRandomMaterialColor();

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
        // Use enemies list copy to avoid removing enemies from original list while iterating through it
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
        Vector3 position = new Vector3(x, 0, transform.position.z);

        return position;
    }
}
