using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform[] spawnPoints;

    public float damageMax = 40f;  // �ִ� ���ݷ�
    public float damageMin = 20f;  // �ּ� ���ݷ�

    public float healthMax = 200f;
    public float healthMin = 100f;

    public float speedMax = 3f;
    public float speedMin = 1.0f;

    public Color strongEnemyColor = Color.red;
    // ���� �� AI�� ������ �� �Ǻλ�
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private int wave;  // ���� ���̺�

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;
        // ���� ��� ����ģ ��� ���� ������ ����
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

    void SpawnWave()  // ���� ���̺꿡 ���� �� ����
    {
        wave++;
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        // ���� ���̺� * 1.5�� �ݿø� �� ����ŭ ����
        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ���� (����)  = 0 ~ 100 �� �������� ����
            float enemyintensity = Random.Range(0f, 1f);
            CreateEnemy(enemyintensity);
        }
    }

    void CreateEnemy(float intensity)  // ���� �����ϰ� ������ ����� �Ҵ�
    {
        // intensity�� ������� ���� �ɷ�ġ�� �����ȴ�.
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);
        // intensity�� ������� �Ͼ���� enemyStrength ���̿��� �� �Ǻλ��� ����
        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);
        // ������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // �� ���������� ���� �� ����
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.Setup(health, damage, speed, skinColor);
        enemies.Add(enemy);
        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        enemy.onDeath += () => GameManager.Instance.AddScore(100);
    }
}
