using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    private Transform _target;
    private Enemy _targetEnemy;

    [Header("General")] public float range = 15f;

    [Header("Use Bullets (default)")] public float fireRate = 1f;
    private float _fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")] public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damagePerSecond = 30;
    public float slowPercent = .5f;


    [Header("Unity Setup Fields")] public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;


    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                _targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }


    void Update()
    {
        if (_target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            // Fire
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = 1f / fireRate;
            }

            _fireCountdown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        _targetEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
        _targetEnemy.Slow(slowPercent);
        
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        var position1 = firePoint.position;
        var position = _target.position;
        
        lineRenderer.SetPosition(0, position1);
        lineRenderer.SetPosition(1, position);
        Vector3 dir = position1 - position;

        impactEffect.transform.position = _target.transform.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LockOnTarget()
    {
        // Lock Target
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(_target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}