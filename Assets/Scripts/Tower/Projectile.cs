using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float explosionRadius;

    [HideInInspector] public float damage;
    [HideInInspector] public EntityType towerProjectileType;
    
    private Transform target;

    [Header("Unity Setup Fields")]
    public GameObject projectileImpactEffect;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);  // This rotates the projectile object while moving towards target
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(projectileImpactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    // Collider essentially gets all objects within the radius.
    // The Physics.OverlapSphere allows for the 'explosion/splash' effect to take place. 
    // It looks at all the objects that are within the explosionRadius of the Projectile.
    // The if statement with the collider.tag allows to only damage the enemies. 
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        float dmgMultiplier = TypeMatchup.GetEffectiveness(e.enemyType, towerProjectileType);

        if (e != null)
        {
            Debug.Log("dmgMulti:" + dmgMultiplier);
            e.TakeDamage(damage * dmgMultiplier);
        }
    }
}
