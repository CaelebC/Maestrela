using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = false;

    [SerializeField] Camera cameraGO;
    
    [SerializeField] float panSpeed;
    [SerializeField] float panBorderThickness;
    [SerializeField] float minX = 24f;
    [SerializeField] float maxX = 46f;
    [SerializeField] float minZ = -42.5f;
    [SerializeField] float maxZ = -32f;

    [SerializeField] float scrollSpeed;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    private float defaultZoom = 30;

    // Update is called once per frame
    void Update()
    {
        // Toggles camera movement
        if (Input.GetKeyDown(KeyCode.Space))
            doMovement = !doMovement;
        
        if (!doMovement)
            return;
        
        CameraPanning();
        CameraZoom();
        
    }

    void CameraPanning()
    {
        Vector3 pos = transform.position;
        
        // Up
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // pos.z += (1 * 0.005f * Time.deltaTime);
            // pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
            // transform.Translate(pos, Space.World);

            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        
        // Down
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        
        // Right
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // Left
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }
    
    void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        defaultZoom -= scroll * 1000 * scrollSpeed * Time.deltaTime; 
        defaultZoom = Mathf.Clamp(defaultZoom, minY, maxY);
        cameraGO.orthographicSize = defaultZoom;
    }
}
