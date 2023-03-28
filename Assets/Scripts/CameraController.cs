using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = false;
    private float zoomLevel = 28;
    private Vector3 startPosition;
    private float startZoomLevel;

    [SerializeField] float panSpeed;
    [SerializeField] float panBorderThickness;

    [SerializeField] float scrollSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;

    [Header("Unity Setup Fields")]
    [SerializeField] CinemachineVirtualCamera virtualCam;

    
    void OnEnable() 
    {        
        startPosition = this.transform.position;
        startZoomLevel = virtualCam.m_Lens.OrthographicSize;
    }
    
    void Update()
    {        
        // Resets camera position and zoom
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = false;  // the OrthographicSize doesn't properly change if doMovement=true

            zoomLevel = startZoomLevel;
            transform.position = startPosition;

            transform.SetPositionAndRotation(startPosition, transform.rotation);
            virtualCam.m_Lens.OrthographicSize = (startZoomLevel);
        }

        // Toggles camera movement
        if (Input.GetKeyDown(KeyCode.Space))
            doMovement = !doMovement;
        
        // For stopping movement if doMovement=false;
        if (!doMovement)
            return;
        
        CameraPanning();
        CameraZoom();
    }

    void CameraPanning()
    {
        
        
        // Up
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
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

        zoomLevel -= scroll * 1000 * scrollSpeed * Time.deltaTime; 
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
        virtualCam.m_Lens.OrthographicSize = zoomLevel;
    }
}
