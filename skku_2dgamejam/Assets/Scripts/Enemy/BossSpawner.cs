using UnityEngine;
using UnityEngine.SceneManagement;

/*public class BossSpawner : MonoBehaviour
{
    public static BossSpawner instance;

    [Header("보스 프리팹")]
    public GameObject bossPrefab;

    [Header("스폰 위치")]
    public Transform spawnPoint;

    [Header("보스 스폰 조건 점수")]
    public int bossSpawnScore = 1000;

    [Header("멈출 일반 Enemy 스포너들")]
    public EnemySpawner[] enemySpawners; // 보스 등장 시 멈출 Enemy 스포너 배열

    private bool bossSpawned = false;

    private void Awake()
    {
        // 싱글톤 설정
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        // 게임 시작 시 점수가 조건 이상이면 보스 소환
        if (ScoreManager.Instance != null)
        {
            CheckSpawnBoss(ScoreManager.Instance.CurrentScore);
        }
    }

    /// <summary>
    /// 점수가 바뀔 때마다 호출
    /// </summary>
    public void CheckSpawnBoss(int currentScore)
    {
        if (bossSpawned) return;

        if (currentScore >= bossSpawnScore)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        bossSpawned = true;

        // 보스 등장 시 일반 Enemy 스포너 멈춤
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
                spawner.StopSpawning();
        }

    }
}*/
