using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject pathPrefab = null;
    [SerializeField] private float spawnCooldown = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool infiniteMoving = false;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetSpawnCooldown()
    {
        return spawnCooldown;
    }

    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public bool GetInfiniteMoving()
    {
        return infiniteMoving;
    }
}