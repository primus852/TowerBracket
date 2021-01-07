using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    public float speed = 10f;
    public int health = 100;
    public int bounty = 50;

    [Header("Effects")]
    public GameObject deathEffect;

    private Transform _target;
    private int _wavepointIndex = 0;

    void Start()
    {
        _target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

        if(Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStats.Money += bounty;
        Destroy(gameObject);
        Destroy(effect, 5f);
    }

    void GetNextWaypoint()
    {

        if(_wavepointIndex >= Waypoints.points.Length - 1)
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
    }


}
