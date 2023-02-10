using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Identity")]
    public string towerName;
    public Sprite towerSprite;
    
    [Header("Tower Stats")]
    public float range;
    public float fireRate;
    private float fireCountdown = 0f;
    public float turnSpeed;
    public int price;
    
    public bool isMPTower;
    public bool IsMPTower{ get{return isMPTower;} }

    [Header("Tower Prefab Setup")]
    public Transform partToRotate;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Tower Upgrade Setup")]
    public Tower upgradedPrefab;
    public int upgradeCost;
    public bool isUpgradeable;

    // For enemy targeting
    private Transform target;
    private Enemy targetEnemy;

    // Tag setups
    [HideInInspector] public string enemyTag = "Enemy";
    [HideInInspector] public string bossTag;

    
    
    void Start()
    {
        isMPTower = false;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public int GetSellPrice()
    {
        return Mathf.RoundToInt(price / 2);
    }

    // Create function to give data for upgrades so that NodeUI can reference properly

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
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

    void Update()
    {
        if (target == null)
            return;

        // Target Lock-on
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, (Time.deltaTime * turnSpeed)).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Firing
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if(projectile != null)
            projectile.Seek(target);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
