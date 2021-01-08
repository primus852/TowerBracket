using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")] public float startSpeed = 10f;
    [HideInInspector] public float speed;
    public float health = 100f;
    public int bounty = 50;

    [Header("Effects")] public GameObject deathEffect;


    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    private void Die()
    {
        GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStats.Money += bounty;
        Destroy(gameObject);
        Destroy(effect, 5f);
    }
}