using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static CanvasManager canvasManager;

    public static PlanetInfoPanel planetInfoPanel;

    void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        planetInfoPanel = GameObject.Find("Canvas").transform.Find("PlanetInfo").GetComponent<PlanetInfoPanel>();
    }
}
