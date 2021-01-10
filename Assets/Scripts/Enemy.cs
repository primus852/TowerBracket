using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")] public float startSpeed = 10f;
    public float startHealth = 100f;
    public int bounty = 50;
    [HideInInspector] public float speed;
    [HideInInspector] public float health;

    [Header("Effects")] public GameObject deathEffect;
    public Image healthBar;


    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
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