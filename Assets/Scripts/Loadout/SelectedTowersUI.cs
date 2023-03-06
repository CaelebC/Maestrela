using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTowersUI : MonoBehaviour
{
    public TowerSpritePrefabScript[] spritePool;
    public Transform parent;
    

    void Awake()
    {
        LoadoutManager.RefreshLoadoutSprites += UpdateSelectedTowersSprites;
    }

    // Update is called once per frame
    void UpdateSelectedTowersSprites(List<Sprite> _towerSpriteList)
    {
        // For clearing the list
        if (_towerSpriteList.Count == 0)
        {
            for (int i = 0; i < spritePool.Length; i++)
            {
                spritePool[i].gameObject.SetActive(false);
            }
            return;
        }
        
        int x = 0;
        foreach (Sprite _towerSprite in _towerSpriteList)
        {
            spritePool[x].gameObject.SetActive(true);
            spritePool[x].GetComponent<TowerSpritePrefabScript>().towerSprite.sprite = _towerSprite;
            x += 1;
        }
    }

    private void OnDestroy() 
    {
        LoadoutManager.RefreshLoadoutSprites -= UpdateSelectedTowersSprites;  
    }
}
