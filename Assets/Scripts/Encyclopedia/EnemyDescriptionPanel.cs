using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDescriptionPanel : MonoBehaviour
{
    [HideInInspector] public Enemy enemyData;
    private bool uiDisplaying = false;

    [Header("Unity Inspector Assignments")]
    public Image enemyImage;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyHealth;
    public TextMeshProUGUI enemySpeed;
    public TextMeshProUGUI enemyType;

    void Start()
    {
        UpdateDescription();
    }
    
    public void UpdateEnemyData(Enemy _enemy)
    {
        enemyData = _enemy.GetComponent<Enemy>();
        UpdateDescription();
    }

    void UpdateDescription()
    {
        // enemyImage.sprite = enemyData.enemySprite;
        enemyName.text = enemyData.enemyName;
        enemyHealth.text = "Health: " + enemyData.startHealth.ToString();
        enemySpeed.text = "Speed: " + enemyData.startSpeed.ToString();
        enemyType.text = "Type: " + enemyData.enemyType.ToString();
    }

    public void TogglePanelItems()
    {
        if (uiDisplaying)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var child = this.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var child = this.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(true);
            }
        }
    }
}
