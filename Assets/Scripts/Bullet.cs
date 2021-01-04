using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    private bool _isTargetNull;

    private void Start()
    {
        _isTargetNull = _target == null;
    }

    public void Seek(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTargetNull)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(_target);
    }

    void HitTarget()
    {
        var transform1 = transform;
        GameObject effectsIns = (GameObject) Instantiate(impactEffect, transform1.position, transform1.rotation);
        Destroy(effectsIns, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_target);
        }


        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Damage(col.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}