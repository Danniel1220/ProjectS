using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static PlanetInfoPanel planetInfoPanel;
    public static PlanetMenuPanel planetMenuPanel;

    void Awake()
    {
        planetInfoPanel = GameObject.Find("Canvas").transform.Find("PlanetInfo").GetComponent<PlanetInfoPanel>();
        planetMenuPanel = GameObject.Find("Canvas").transform.Find("PlanetMenu").GetComponent<PlanetMenuPanel>();
    }
}
