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
    
    [SerializeField] private bool isMPTower;
    public bool IsMPTower{ get{return isMPTower;} }

    [Header("Tower Prefab Setup")]
    public Transform partToRotate;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Tower Upgrade Setup")]
    [SerializeField] private bool isUpgradeable;
    public bool IsUpgradeable{ get{return isUpgradeable;} }
    public int currentUpgradeLevel = 0;

    public Tower upgradePrefab1;  // Damage +
    public Tower upgradePrefab2;  // Fire rate +
    public Tower upgradePrefab3;  // Range +

    public int upgradeCost1;  // Damage +
    public int upgradeCost2;  // Fire rate +
    public int upgradeCost3;  // Range +

    [HideInInspector] public int upgradesSpent;

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
        return Mathf.RoundToInt( (upgradesSpent + price) / 2 );
    }

    // Returns (upgradeCostX, upgradePrefabX), where X = currentUpgradeLevel + 1
    public (int, Tower) GetUpgradePath()
    {  
        if (isUpgradeable && currentUpgradeLevel < 3)
        {
            List<int> tempCost = new List<int>{upgradeCost1, upgradeCost2, upgradeCost3};
            List<Tower> tempPrefab = new List<Tower>{upgradePrefab1, upgradePrefab2, upgradePrefab3};
            return (tempCost[currentUpgradeLevel], tempPrefab[currentUpgradeLevel]);
        }
        else
            return (-1, upgradePrefab3);  // This should never be returned. 
        
    }

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
