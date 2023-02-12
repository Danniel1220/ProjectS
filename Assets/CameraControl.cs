using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float cameraPanSpeed = 0.2f;
    float cameraZoomSpeed = 200f;

    public float zoomMinDistance = 200f;
    public float zoomMaxDistance = 2000f;
    public float zoomSpeed = 100f;
    public float zoomFactor = 100f;
    public float zoomVelocity = 0f;

    float zoomSmoothingFactor = 5f;

    public float currentZoom = 800f;
    public float targetZoom = 800f;

    Vector3 currentCameraOffset;
    Vector3 targetCameraOffset;

    Vector3 lastMousePosition;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineCameraOffset cinemachineCameraOffset;
    CinemachineTransposer cinemachineTransposer;
    Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;

    Transform cameraAnchor;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        //cinemachineCameraOffset = cinemachineVirtualCamera.GetComponent<CinemachineExtension>().GetComponent<CinemachineCameraOffset>();
        cinemachineTransposer = cinemachineVirtualCamera.GetComponentInChildren<CinemachineTransposer>();
        cinemachine3RdPersonFollow = cinemachineVirtualCamera.GetComponentInChildren<Cinemachine3rdPersonFollow>();
        cameraAnchor = GameObject.Find("Starship").transform.Find("CameraAnchor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            cameraAnchor.Rotate(-delta.y * cameraPanSpeed, delta.x * cameraPanSpeed, 0);
            lastMousePosition = Input.mousePosition;
        }

        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomFactor;
        targetZoom -= zoomDelta; // subtracting value because mwheel inverse logic for zoom is prefered
        targetZoom = Mathf.Clamp(targetZoom, zoomMinDistance, zoomMaxDistance); // limiting zoom to be between min/max values
        cinemachine3RdPersonFollow.CameraDistance = Mathf.Lerp(cinemachine3RdPersonFollow.CameraDistance, targetZoom, Time.deltaTime * zoomSmoothingFactor); // smoothing zoom
    }
}
