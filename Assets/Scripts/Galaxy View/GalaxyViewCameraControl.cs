using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewCameraControl : MonoBehaviour
{
    private Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;
    private StarSystemViewManager starSystemViewManager;

    private Transform cameraAnchor;
    private Transform cameraAnchorLookPoint;

    private Vector3 lastMousePosition;
    [SerializeField] private float cameraAnchorLookPointYPos;
    [SerializeField] private float cameraMaxYClamp = 200f;
    [SerializeField] private float cameraMinYClamp = -200f;

    [SerializeField] private float zoomMinDistance = 1000f;
    [SerializeField] private float zoomMaxDistance = 100000f;
    [SerializeField] private float zoomMinMultiplier = 100f;
    [SerializeField] private float zoomMaxMultiplier = 1000f;
    [SerializeField] private float zoomCurrentMultiplier = 100f;
    [SerializeField] private float zoomSmoothingFactor = 5f;
    [SerializeField] private float zoomDelta;
    [SerializeField] private float targetZoom = 1000f;
    [SerializeField] private float starViewZoomThreshhold = 250f;

    private bool isInStarViewRange = false;

    void Start()
    {
        cinemachine3RdPersonFollow = GetComponentInChildren<Cinemachine3rdPersonFollow>();
        starSystemViewManager = GameObject.Find("Starship").GetComponent<StarSystemViewManager>();

        cameraAnchor = GameObject.Find("Starship").transform.Find("CameraAnchor").GetComponent<Transform>();
        cameraAnchorLookPoint = GameObject.Find("Starship").transform.Find("CameraAnchorLookPoint").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        handleCameraRotation();
        handleZoom();
        // so we can zoom in/out faster the further out we are
        mapZoomMultiplierToCurrentZoomDistance();
        if (cinemachine3RdPersonFollow.CameraDistance < starViewZoomThreshhold && isInStarViewRange == false) enterStarView();
        if (cinemachine3RdPersonFollow.CameraDistance >= starViewZoomThreshhold && isInStarViewRange == true) exitStarView();
    }

    void LateUpdate()
    {
        cameraAnchor.LookAt(cameraAnchorLookPoint.position);
    }

    public void handleCameraRotation()
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
    }

    public void handleZoom()
    {
        zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomCurrentMultiplier;
        targetZoom -= zoomDelta; // subtracting value because mwheel inverse logic for zoom is prefered
        targetZoom = Mathf.Clamp(targetZoom, zoomMinDistance, zoomMaxDistance); // limiting zoom to be between min/max values
        cinemachine3RdPersonFollow.CameraDistance = Mathf.Lerp(cinemachine3RdPersonFollow.CameraDistance, targetZoom, Time.deltaTime * zoomSmoothingFactor); // smoothing zoom
    }

    public void mapZoomMultiplierToCurrentZoomDistance()
    {
        float t = Mathf.InverseLerp(zoomMinDistance, zoomMaxDistance, targetZoom);
        float output = Mathf.Lerp(zoomMinMultiplier, zoomMaxMultiplier, t);
        zoomCurrentMultiplier = output;
    }

    public void enterStarView()
    {
        isInStarViewRange = true;
        starSystemViewManager.enterStarSystemView();
    }

    public void exitStarView()
    {
        isInStarViewRange = false;
        starSystemViewManager.exitStarSystemView();
    }
}
