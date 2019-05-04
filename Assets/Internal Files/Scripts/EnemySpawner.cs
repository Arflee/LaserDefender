using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs = null;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool looping = false;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnEnemies(waveConfigs[i]));
        }
    }

    private IEnumerator SpawnEnemies(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            GameObject enemy = Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].position,
                Quaternion.identity
            );

            enemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.GetSpawnCooldown());
        }
    }
}