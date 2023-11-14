using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField][NonReorderable] GameObject[] MonsterSpawner;

        public GameObject[] GetMonsterSpawnList()
        {
            return MonsterSpawner;
        }
    }

    [SerializeField][NonReorderable] WaveContent[] waves;
    int currentWave = 0;
    [SerializeField]float spawnRange = 0.5f;
    public int enemiesKilled;
    public List<GameObject> currentMonster;


    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMonster.Count == 0)
        {
            enemiesKilled = 0;
            currentWave++;
            SpawnWave();
        }
    }
    /// <summary>
    /// Method for drawing a spawn wange sphere
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, spawnRange);
    }
    void SpawnWave()
    {
        
        for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
        {
            GameObject newSpawn = Instantiate(waves[currentWave].GetMonsterSpawnList()[i], FindSpawnLoc(), Quaternion.identity);
            currentMonster.Add(newSpawn);

            Enemy monster = newSpawn.GetComponent<Enemy>();
            monster.SetSpawner(this);
        }
    }
    Vector3 FindSpawnLoc()
    {
        Vector3 spawnPos;

        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = transform.position.y;

        spawnPos = new Vector3(xLoc, yLoc, zLoc);

        if(Physics.Raycast(spawnPos, Vector3.down, 2))
        {
            return spawnPos;
        }
        else
        {
            return FindSpawnLoc();
        }
    }
}
