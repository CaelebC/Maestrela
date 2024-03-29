using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public string enemyName;
    public Sprite enemySprite;
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int enemyDamage = 10;
    public EntityType enemyType;
    [HideInInspector] public string typeHexColor;

    [HideInInspector] public bool isMinion;
    [HideInInspector] public float speed;
    [HideInInspector] public float health;
    private bool alreadyDefeated = false;
    [HideInInspector] public float dmgMulti = 1f;

    [Header("Unity Setup Fields")]
    [SerializeField] Image healthBar;
    [SerializeField] Image enemyGameObjImage;
    [SerializeField] Image enemyGameObjGlow;
    public GameObject defeatParticle;
    private Color enemyGlowColor;
    

    void Awake()
    {
        speed = startSpeed * PlayerStats.enemyMoveSpeedMultiplier;
        health = startHealth * PlayerStats.enemyHPMultiplier;
        
        typeHexColor = EntityTypeColor.TypeColor(enemyType);
        ColorUtility.TryParseHtmlString(typeHexColor, out enemyGlowColor);
        
        enemyGameObjImage.sprite = enemySprite;
        enemyGameObjGlow.sprite = enemySprite;
        enemyGameObjGlow.color = enemyGlowColor;

    }

    public void TakeDamage(float amount)
    {
        health -= (amount * dmgMulti);

        healthBar.fillAmount = health / startHealth;  // for UI healthbar

        if(health <= 0 && !alreadyDefeated)
        {
            EnemyDefeated();
            alreadyDefeated = true;
        }
    }

    public void Slow(float _slowPercentage)
    {
        speed = startSpeed * ( PlayerStats.enemyMoveSpeedMultiplier - _slowPercentage );
    }

    public void DamageAmplify(float _newDmgMulti)
    {
        dmgMulti = ( 1f + _newDmgMulti );
    }

    public virtual void EnemyDefeated()
    {
        WaveSpawner.enemiesAlive--;

        GameObject defeatEffect = (GameObject)Instantiate(defeatParticle, transform.position, Quaternion.identity);
        
        Destroy(defeatEffect, 3f);
        Destroy(gameObject);
    }
}
