using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform[] spawnPoints;

    public float damageMax = 40f;  // 최대 공격력
    public float damageMin = 20f;  // 최소 공격력

    public float healthMax = 200f;
    public float healthMin = 100f;

    public float speedMax = 3f;
    public float speedMin = 1.0f;

    public Color strongEnemyColor = Color.red;
    // 강한 적 AI가 가지게 될 피부색
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private int wave;  // 현재 웨이브

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;
        // 적을 모두 물리친 경우 다음 스판을 실행
        if (enemies.Count <= 0)
        {
            SpawnWave();
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        UIManager.Instance.UpdateWaveText(wave, enemies.Count);
    }

    void SpawnWave()  // 현재 웨이브에 맞춰 적 생성
    {
        wave++;
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        // 현재 웨이브 * 1.5를 반올림 한 수만큼 생성
        for (int i = 0; i < spawnCount; i++)
        {
            // 적의 강도 (세기)  = 0 ~ 100 중 랜덤으로 결정
            float enemyintensity = Random.Range(0f, 1f);
            CreateEnemy(enemyintensity);
        }
    }

    void CreateEnemy(float intensity)  // 적을 생성하고 추적할 대상을 할당
    {
        // intensity를 기반으로 적의 능력치가 결정된다.
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);
        // intensity를 기반으로 하얀색과 enemyStrength 사이에서 적 피부색이 결정
        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);
        // 생설할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // 적 프리팹으로 부터 적 생성
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.Setup(health, damage, speed, skinColor);
        enemies.Add(enemy);
        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        enemy.onDeath += () => GameManager.Instance.AddScore(100);
    }
}
