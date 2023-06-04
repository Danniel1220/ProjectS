using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenuPanel : MonoBehaviour
{
    private PlanetInfoPanel planetInfoPanel;
    private Button closeWindowButton;

    void Start()
    {
        planetInfoPanel = UIManagers.planetInfoPanel;

        closeWindowButton = this.transform.Find("CloseWindowButton").GetComponent<Button>();

        this.gameObject.SetActive(false);
    }

    public void closeWindow()
    {
        this.gameObject.SetActive(false);
        planetInfoPanel.openPlanetMenuButton.enabled = true;
    }
}
