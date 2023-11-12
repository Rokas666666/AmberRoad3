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
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnWave()
    {
        for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
        {
            Instantiate(waves[currentWave].GetMonsterSpawnList()[i], FindSpawnLoc(), Quaternion.identity);
        }
    }
    Vector3 FindSpawnLoc()
    {
        Vector3 spawnPos;

        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = transform.position.y;

        spawnPos = new Vector3(xLoc, yLoc, zLoc);

        if(Physics.Raycast(spawnPos, Vector3.down, 5))
        {
            return spawnPos;
        }
        else
        {
            return FindSpawnLoc();
        }
    }
}
