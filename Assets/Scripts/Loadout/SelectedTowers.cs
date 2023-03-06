using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTowers : MonoBehaviour
{
    // public GameObject towerSpritePrefab;
    public TowerSpritePrefabScript[] spritePool;
    public Transform parent;
    

    void Awake()
    {
        LoadoutManager.RefreshLoadoutSprites += UpdateSelectedTowersSprites;
    }

    // Update is called once per frame
    void UpdateSelectedTowersSprites(List<Sprite> a)
    {
        // For clearing the list
        if (a.Count == 0)
        {
            for (int i = 0; i < spritePool.Length; i++)
            {
                spritePool[i].gameObject.SetActive(false);
            }
            return;
        }
        
        int x = 0;
        foreach (Sprite _towerSprite in a)
        {
            spritePool[x].gameObject.SetActive(true);
            spritePool[x].GetComponent<TowerSpritePrefabScript>().towerSprite.sprite = _towerSprite;
            x += 1;
        }
    }
}
