using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    float zoomMinDistance = 5f;
    float zoomMaxDistance = 2000f;
    float zoomMultiplier = 100f;
    float zoomSmoothingFactor = 5f;
    float zoomDelta;
    float targetZoom = 20f;

    Vector3 lastMousePosition;

    CinemachineVirtualCamera cinemachineVirtualCamera;
    Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;

    Transform cameraAnchor;
    Transform cameraAnchorLookPoint;

    float cameraAnchorLookPointYPos;
    float cameraMaxYClamp = 200f;
    float cameraMinYClamp = -200f;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        cinemachine3RdPersonFollow = cinemachineVirtualCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>();
        
        cameraAnchor = GameObject.Find("Starship").transform.Find("CameraAnchor").GetComponent<Transform>();
        cameraAnchorLookPoint = GameObject.Find("Starship").transform.Find("CameraAnchorLookPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if right mouse button is pressed down, store the position where the click happened
        if (Input.GetMouseButtonDown(1)) lastMousePosition = Input.mousePosition;

        // if right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // calculate the distance it moves on each axis since it was first pressed down
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // if the mouse moved on y axis on screen move the anchor look point up or down
            if (delta.y != 0)
            {
                cameraAnchorLookPoint.position = new Vector3(
                    cameraAnchorLookPoint.position.x, 
                    Mathf.Clamp(cameraAnchorLookPoint.position.y + delta.y, cameraAnchor.position.y + cameraMinYClamp, cameraAnchor.position.y + cameraMaxYClamp), // camera is clamped on the Y angle to prevent weird interactions with the anchor
                    cameraAnchorLookPoint.position.z);

            }
            // if the mouse moved on x axis on screen, rotate the anchor look point around the anchor
            if (delta.x != 0)
            {
                cameraAnchorLookPoint.RotateAround(cameraAnchor.position, Vector3.up, delta.x * Time.deltaTime * 100);
            }
        }

        zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier;
        targetZoom -= zoomDelta; // subtracting value because mwheel inverse logic for zoom is prefered
        targetZoom = Mathf.Clamp(targetZoom, zoomMinDistance, zoomMaxDistance); // limiting zoom to be between min/max values
        cinemachine3RdPersonFollow.CameraDistance = Mathf.Lerp(cinemachine3RdPersonFollow.CameraDistance, targetZoom, Time.deltaTime * zoomSmoothingFactor); // smoothing zoom

        //place another object that the anchor looks at, move that anchor up/down clamped and around the anchor for y axis
    }

    void LateUpdate()
    {
        cameraAnchor.LookAt(cameraAnchorLookPoint.position);
    }
}
