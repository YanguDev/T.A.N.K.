using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyChance> enemiesList;
    [SerializeField] private Transform floor;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnPositionMargin;

    private List<Enemy> spawnedEnemies = new List<Enemy>();

    private bool canSpawn;

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

        Enemy newEnemy = Instantiate(GetRandomEnemy(), GetRandomSpawnPosition(), transform.rotation);
        newEnemy.transform.OffsetYByScale(floor);
        newEnemy.transform.SetParent(transform, true);
        RandomizeEnemyColor(newEnemy);

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
    public List<Enemy> GetEnemies(){
        return spawnedEnemies;
    }

    private Enemy GetRandomEnemy(){
        Enemy enemy = null;
        int c = Random.Range(1, 101);
        int chanceSum = 0;

        for(int i = 0; i < enemiesList.Count; i++){
            int chance = enemiesList[i].chance;
            if(c > chanceSum && c <= chanceSum + chance){
                enemy = enemiesList[i].enemy;
                break;
            }else{
                chanceSum += chance;
            }
        }

        return enemy;
    }

    private Vector3 GetRandomSpawnPosition(){
        float maxHorizontalPosition = transform.localScale.x/2 - spawnPositionMargin;
        float x = Random.Range(-maxHorizontalPosition, maxHorizontalPosition);
        Vector3 position = new Vector3(x, 0, transform.position.z);

        return position;
    }

    private void RandomizeEnemyColor(Enemy enemy){
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);

        enemy.GetComponent<MeshRenderer>().material.color = color;
    }
}

public static class VectorHelper{
    public static void OffsetYByScale(this Transform t, Transform floor){
        Vector3 pos = t.position;
        pos.y = t.localScale.y/2 + floor.localScale.y/2;
        t.position = pos;
    }
}
