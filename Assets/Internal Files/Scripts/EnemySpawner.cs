using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs = null;
    private int startingWave = 0;

    private void Start()
    {
        WaveConfig currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnEnemies(currentWave));
    }

    private IEnumerator SpawnEnemies(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(currentWave.GetSpawnCooldown());
        }
    }
}
