using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    int cursorOffsetY = 3;
    public static HoverUI hoverUIInstance;
    
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
    public void Activate(Sprite _sprite)
    {
        spriteRenderer.enabled = true;
        this.spriteRenderer.sprite = _sprite;
    }

    // This is ran in the Node.BuildTower()
    public void Deactivate()
    {
        spriteRenderer.enabled = false;
    }
}
