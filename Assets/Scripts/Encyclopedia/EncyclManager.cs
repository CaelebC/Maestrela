using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclManager : MonoBehaviour
{
    [SerializeField] bool isEnemyEncyclopedia;

    [Header("Unity Setup Fields")]
    [SerializeField] EncyclContent content;
    public EncyclButton[] buttonPool;
    public GameObject buttonContainer;
    public DescriptionPanel descPanel;
    
    void OnEnable()    
    {
        // for Tower encyclopedia
        if (!isEnemyEncyclopedia)
        {
            int i = 0;
            foreach (Tower _tower in content.allTowers)
            {
                buttonPool[i].gameObject.SetActive(true);

                buttonPool[i].encyclManager = this;
                buttonPool[i].towerData = _tower;
                buttonPool[i].itemName.text = _tower.towerName;
                buttonPool[i].itemType.text = _tower.TowerEntityType.ToString();
                                
                i += 1;
            }
        }

        // for Enemy encyclopedia
        else if (isEnemyEncyclopedia)
        {
            int i = 0;
            foreach (Enemy _enemy in content.allEnemies)
            {
                buttonPool[i].gameObject.SetActive(true);

                buttonPool[i].encyclManager = this;
                buttonPool[i].enemyData = _enemy;
                buttonPool[i].itemName.text = _enemy.enemyName;
                buttonPool[i].itemType.text = _enemy.enemyType.ToString();
                
                i += 1;
            }
        }
    }

    public void UpdateDescription(Tower _tower)
    {
        descPanel.UpdateTowerData(_tower);
    }
}
