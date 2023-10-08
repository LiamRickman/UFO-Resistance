using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    [SerializeField] Transform partToRotate;

    [Header("Stats")]
    public float range = 15f;
    public string special = "None";
    [SerializeField] string turretType;

    [Header("Projectile Values (default)")]
    [SerializeField] GameObject projectilePrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField] int projectileDamage = 20;
    [SerializeField] float projectileSpeed = 50f;
    [SerializeField] float projectileRadius = 0f;
    [SerializeField] bool isRocket = false;
    [SerializeField] bool isBurst = false;
    [SerializeField] int burstShots = 0;
    private int burstIndex;
    [SerializeField] float reloadTime;
    private float currentReloadTime;

    [Header("Laser Values")]
    [SerializeField] bool isLaser = false;
    [SerializeField] int damageOverTime = 30;
    [SerializeField] float slowAmount = 0.5f;
    [SerializeField] float laserAOERadius = 0f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] Light impactLight;
    
    [Header("Unity Setup Fields")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] float turnSpeed = 10f;


    [SerializeField] Transform firePoint;


    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //Temporary Variables
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (isLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }

        LockOnTarget();

        if (isLaser)
        {
            FireLaser();
        }
        else if (isBurst)
        {
            if (currentReloadTime <= 0)
            {
                if (burstIndex != burstShots)
                {
                    if (fireCountdown <= 0f)
                    {
                        Shoot();
                        fireCountdown = 1f / fireRate;
                        burstIndex++;
                    }
                    fireCountdown -= Time.deltaTime;
                }
                else
                {
                    currentReloadTime = reloadTime;
                }
            }
            else
            {
                burstIndex = 0;
                currentReloadTime -= Time.deltaTime;
            }
        }
        else
        {
            //Shooting standard bullet
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    //Calculating turret rotation
    private void LockOnTarget()
    {
        Vector3 direction = target.position - partToRotate.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void FireLaser()
    {
        if (laserAOERadius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(target.position, laserAOERadius);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    enemy.SetTurretType(turretType);
                    enemy.TakeDamage(damageOverTime * Time.deltaTime);
                    enemy.Slow(slowAmount);
                }
            }
        }
        else
        {
            targetEnemy.SetTurretType(turretType);
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
            targetEnemy.Slow(slowAmount);
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;

        impactEffect.transform.position = target.position + direction.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        TurretProjectile projectile = projectileGO.GetComponent<TurretProjectile>();
        projectile.SetTurretType(turretType);

        if (isRocket)
        {
            SoundManager.PlaySound("RocketFire");
        }
        else
        {
            SoundManager.PlaySound("MGFire");
        }
        

        //Set Values
        projectile.damage = projectileDamage;
        projectile.speed = projectileSpeed;
        projectile.explosionRadius = projectileRadius;

        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

    public float GetDamage()
    {
        if (!isLaser)
        {
            return projectileDamage;
        }
        else
        {
            return damageOverTime;
        }
    }

    public float GetFirerate()
    {
        return fireRate;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetSlow()
    {
        return slowAmount;
    }

    public float GetExplosionRadius()
    {
        return projectileRadius;
    }

    public string GetSpecial()
    {
        return special;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
