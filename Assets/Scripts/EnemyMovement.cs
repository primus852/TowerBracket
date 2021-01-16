using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _wavepointIndex = 0;
    private Enemy _enemy;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * (_enemy.speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        _enemy.speed = _enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.Lives--;
        WaveSpawn.EnemiesAlive--;
    }
}