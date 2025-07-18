using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
public class EnemySpawnInfo {
    public GameObject enemyPrefab;
    public int amount;
}

[Serializable]
public class Wave {
    public List<EnemySpawnInfo> enemies = new();
}


public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves = new();
    public Transform spawnPoint;
    public float delayBetweenSpawns = 0.5f;

    public event Action<int> OnWaveStarted;
    public event Action OnAllWavesCompleted;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private int aliveEnemyCount = 0;

    public void StartWaves()
    {
        if (!isSpawning)
            StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        isSpawning = true;

        while (currentWaveIndex < waves.Count)
        {
            OnWaveStarted?.Invoke(currentWaveIndex + 1);
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            yield return new WaitUntil(() => aliveEnemyCount == 0);

            currentWaveIndex++;
        }

        OnAllWavesCompleted?.Invoke();
        isSpawning = false;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        aliveEnemyCount = 0;

        foreach (var spawnInfo in wave.enemies)
        {
            for (int i = 0; i < spawnInfo.amount; i++)
            {
                GameObject enemy = Instantiate(spawnInfo.enemyPrefab, spawnPoint.position, Quaternion.identity);

                Stats stats = enemy.GetComponent<Stats>();
                if (stats != null)
                {
                    aliveEnemyCount++;

                    stats.OnDeath += OnEnemyDied;
                }

                yield return new WaitForSeconds(delayBetweenSpawns);
            }
        }
    }

    private void OnEnemyDied(GameObject attacker)
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);
    }

    public void ResetWaves()
    {
        currentWaveIndex = 0;
    }

    public bool AllWavesCompleted => currentWaveIndex >= waves.Count;
}
