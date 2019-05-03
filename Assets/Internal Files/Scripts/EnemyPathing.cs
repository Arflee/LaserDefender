using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig waveConfig;
    private List<Transform> waypoints = new List<Transform>();
    private int waypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        MoveToWaypoint();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveToWaypoint()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;

            float moveDelta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);
            if ((Vector2)transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            if (waveConfig.GetInfiniteMoving())
            {
                waypointIndex = 0;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
