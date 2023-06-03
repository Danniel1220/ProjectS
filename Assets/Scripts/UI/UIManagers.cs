using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static CanvasManager canvasManager;

    public static PlanetInfoPanel planetInfoPanel;
    public static PlanetMenuPanel planetMenuPanel;


    void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        planetInfoPanel = GameObject.Find("Canvas").transform.Find("PlanetInfo").GetComponent<PlanetInfoPanel>();
        planetMenuPanel = GameObject.Find("Canvas").transform.Find("PlanetMenu").GetComponent<PlanetMenuPanel>();
    }
}
