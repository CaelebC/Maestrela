using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// This script probably would've been better if it was all in the DescriptionPanel, but oh well it's already here and close to the deadline already.
public class EnemyDescriptionPanel : MonoBehaviour
{
    [HideInInspector] public Enemy enemyData;
    private bool uiDisplaying = false;
    private string hexTextColor;

    [Header("Unity Inspector Assignments")]
    public Image enemyImage;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyHealth;
    public TextMeshProUGUI enemySpeed;
    public TextMeshProUGUI enemyType;
    public TextMeshProUGUI enemyDamageToHP;

    void Start()
    {
        // UpdateDescription();
    }
    
    public void UpdateEnemyData(Enemy _enemy)
    {
        enemyData = _enemy.GetComponent<Enemy>();
        hexTextColor = "";
        UpdateEnemyDescription();
    }

    void UpdateEnemyDescription()
    {
        hexTextColor = EntityTypeColor.TypeColor(enemyData.enemyType);

        // enemyImage.sprite = enemyData.enemySprite;
        enemyName.text = enemyData.enemyName;
        enemyHealth.text = "Health: " + enemyData.startHealth.ToString();
        enemySpeed.text = "Speed: " + enemyData.startSpeed.ToString();
        enemyType.text = "Type: <color="+hexTextColor+">" + enemyData.enemyType.ToString() + "</color>";
        enemyDamageToHP.text = "Damage to HP: " + enemyData.enemyDamage.ToString();
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
