using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour
{
    public static HoverUI hoverUIInstance;
    
    [SerializeField] SpriteRenderer towerSprite;
    [SerializeField] SpriteRenderer circleRangeSprite;

    int cursorOffsetY = 3;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        hoverUIInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        // If errors occur, this is because the camera in the scene NEEDS to have the
        // tag named 'MainCamera' in order for errors to go away.
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        transform.position = new Vector3(transform.position.x, cursorOffsetY, transform.position.z);
    }

    // This is ran in the ShopPage.BuildTowerButton()
    public void Activate(Tower _tower)
    {
        Sprite spriteTower = _tower.towerSprite;
        float rangeTower = _tower.Range * 2;
        
        towerSprite.enabled = true;
        circleRangeSprite.enabled = true;
        this.towerSprite.sprite = spriteTower;
        this.circleRangeSprite.transform.localScale = new Vector3(rangeTower, rangeTower, 0);
    }

    // This is ran in the Node.BuildTower(), and in many parts of Node.cs
    public void Deactivate()
    {
        towerSprite.enabled = false;
        circleRangeSprite.enabled = false;
    }
}
