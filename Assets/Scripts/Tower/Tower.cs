using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Identity")]
    public string towerName;
    public Sprite towerSprite;
    [SerializeField] int cost;
    [SerializeField] float buyCooldown;
    [SerializeField] bool isMPTower;

    [Header("DMG Tower Stats")]
    [SerializeField] float damage;
    [SerializeField] RangeType range;
    [SerializeField] float fireRate;
    [SerializeField] EntityType towerEntityType;
    [SerializeField] AttackType towerAttackType;
    
    private float turnSpeed = 20;
    private float fireCountdown = 0f;

    private float startingDamage;
    private float startingBuyCooldown;

    public float Damage{ get{return damage;} }
    public float FireRate{ get{return fireRate;} }
    public float Range{ get{return (float)range;} }

    public int Cost{ get{return cost;} }
    public bool IsMPTower{ get{return isMPTower;} }
    public float BuyCooldown{ get{return buyCooldown;} }
    public EntityType TowerEntityType{ get{return towerEntityType;} }
    public AttackType TowerAttackType{ get{return towerAttackType;} }

    [Header("DMG Tower Prefab Setup")]
    [SerializeField] GameObject rangeCircleSprite;
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public LineRenderer laserLineRenderer;  // Line Renderer should be a component in the tower
    
    [Header("DMG Tower Upgrade Setup")]
    [SerializeField] bool isUpgradeable;
    public bool IsUpgradeable{ get{return isUpgradeable;} }
    public int currentUpgradeLevel = 0;

    public Tower upgradePrefab1;  // Damage +
    public Tower upgradePrefab2;  // Fire rate +
    public Tower upgradePrefab3;  // Range +

    public int upgradeCost1;  // Round(cost * 0.3)
    public int upgradeCost2;  // Round(cost * 0.7)
    public int upgradeCost3;  // Round(cost * 1.2)

    // For enemy targeting
    private Transform target;
    private Enemy targetEnemy;

    // Tag setups
    [HideInInspector] public string enemyTag = "Enemy";
    [HideInInspector] public string bossTag;
    
    
    void Start()
    {
        isMPTower = false;
        startingDamage = damage;
        startingBuyCooldown = buyCooldown;
        if (rangeCircleSprite)
        {
            rangeCircleSprite.SetActive(false);
            rangeCircleSprite.transform.localScale = new Vector3(Range*2, Range*2, 0f);  
            // range*2 because the actual range is bigger than it is here in the rangeCircle's XY scale.
            // The scale of the actual tower prefab NEEDS to be set to (1,1,1) in order for this to properly work.
            // This is because localScale relies on the parent's scale, in this case the parent is the tower itself.
        }
        
        upgradeCost1 = Mathf.RoundToInt(Cost * 0.3f);
        upgradeCost2 = Mathf.RoundToInt(Cost * 0.7f);
        upgradeCost3 = Mathf.RoundToInt(Cost * 1.2f);

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        MPManager.OnBurnout += BurnoutDamage;
        MPManager.OnRecover += RecoveryDamage;
        PlayerStats.numBuiltTowers += 1;
    }

    void OnDestroy()
    {
        MPManager.OnBurnout -= BurnoutDamage;
        MPManager.OnRecover -= RecoveryDamage;
        PlayerStats.numBuiltTowers -= 1;
    }

    // OnDrawGizmosSelected() to see the range of the tower when selected ONLY IN EDITOR
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    public void ToggleTowerRange()
    {
        if (!rangeCircleSprite)  // Specifically for MP towers where they don't have a range
        {
            return;
        }
        
        if (rangeCircleSprite.activeSelf)
        {
            rangeCircleSprite.SetActive(false);
            return;
        }
        else if (!rangeCircleSprite.activeSelf)
        {
            rangeCircleSprite.SetActive(true);
            return;
        }
    }

    public int GetSellPrice()
    {
        int upgradesSpent = 0;
        if (currentUpgradeLevel >= 1)
            upgradesSpent += upgradeCost1;
        if (currentUpgradeLevel >= 2)
            upgradesSpent += upgradeCost2;
        if (currentUpgradeLevel >= 3)
            upgradesSpent += upgradeCost3;
        
        return Mathf.RoundToInt( (upgradesSpent + cost) / 2 );
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

    void Update()
    {
        if (target == null)
        {
            if ((TowerAttackType == AttackType.Laser) && laserLineRenderer.enabled)
                laserLineRenderer.enabled = false;
            return;
        }

        LockOnTarget();

        if (TowerAttackType == AttackType.Projectile)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        else if (TowerAttackType == AttackType.Laser)
        {
            Laser();
        }
        else if (TowerAttackType == AttackType.Spiker)
        {
            // ShootSpikes();
        }
    }

    // Changes the target enemy
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

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Aims at the enemy
    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, (Time.deltaTime * turnSpeed)).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.towerProjectileType = this.towerEntityType;
        projectile.damage = this.damage;
        // Debug.Log("damage:" + damage);

        if(projectile != null)
            projectile.Seek(target);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damage * Time.deltaTime);
        
        if (!laserLineRenderer.enabled)
            laserLineRenderer.enabled = true;
        
        laserLineRenderer.SetPosition(0, firePoint.position);
        laserLineRenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;
    }

    void BurnoutDamage(float _dmgMulti)
    {  
        damage = startingDamage * _dmgMulti;
    }

    void RecoveryDamage(float _dmgMulti)
    {
        damage = startingDamage * _dmgMulti;
    }


}
