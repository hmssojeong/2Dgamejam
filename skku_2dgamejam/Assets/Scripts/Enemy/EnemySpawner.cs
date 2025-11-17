using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs;
    public List<int> EnemySpawnRate;


    public float CoolTime = 3f;
    private float _timer;

    public bool CanSpawn = true;

    private void Start()
    {
        // 쿨타임을 1과 2사이로 랜덤하게 지정한다.
        float randomCoolTime = Random.Range(1f, 5f);
        CoolTime = randomCoolTime;
    }

 
    private void Update()
    {
        if (!CanSpawn) return;

        // 1. 시간이 흐르다가.
        _timer += Time.deltaTime;

        // 2. 쿨타임이 되면
        if (_timer >= CoolTime)
        {
            _timer = 0f;


            GameObject enemyObject = Instantiate(SelectRamdomEnemy());       // 70%
            enemyObject.transform.position = transform.position;

        }
    }

/*    public void StopSpawning()
    {
        CanSpawn = false;
    }*/


    private GameObject SelectRamdomEnemy()
    {
        int randomNumber = Random.Range(1, 101);

        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            randomNumber -= EnemySpawnRate[i];

            if (randomNumber <= 0)
            {
                // 95        70  
                //    25     20
                //     5      10  =i
                return EnemyPrefabs[i];
            }

        }

        return EnemyPrefabs[0];
    }
}
