using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
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
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);  // This rotates the projectile object while moving towards target
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
        Destroy(enemy.gameObject);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
