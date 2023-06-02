using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private GameObject planetInfoPanel;

    void Start()
    {
        planetInfoPanel = UIManagers.planetInfoPanel.GetComponent<GameObject>();
    }

    public void enablePlanetInfoPanel()
    {
        planetInfoPanel.SetActive(true);
    }

    public void disablePlanetInfoPanel()
    {
        planetInfoPanel.SetActive(false);
    }
}
