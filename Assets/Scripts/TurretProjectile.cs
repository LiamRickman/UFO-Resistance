using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class TurretProjectile : MonoBehaviour
{
    private Transform target;

    
    public GameObject bulletImpact;

    [HideInInspector] public float speed = 70f;

    [HideInInspector] public int damage = 50;
    [HideInInspector] public float explosionRadius = 0f;

    private string turretType = null;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject bulletImpactInstance = (GameObject)Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(bulletImpactInstance, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Damage(Transform enemy)
    {
        Enemy currentEnemy = enemy.GetComponent<Enemy>();

        if (currentEnemy != null)
        {
            currentEnemy.SetTurretType(turretType);
            currentEnemy.TakeDamage(damage);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.red;
    }

    public void SetTurretType(string turret)
    {
        turretType = turret;
    }
}
