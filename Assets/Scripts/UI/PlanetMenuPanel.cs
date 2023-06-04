using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenuPanel : MonoBehaviour
{
    private StarshipPosition starshipPosition;

    private PlanetInfoPanel planetInfoPanel;

    private TextMeshProUGUI planetNameText;

    private Button closeWindowButton;

    void Start()
    {
        starshipPosition = GameManagers.starshipPosition;

        planetInfoPanel = UIManagers.planetInfoPanel;

        planetNameText = this.transform.Find("PlanetNameText").GetComponent<TextMeshProUGUI>();
        closeWindowButton = this.transform.Find("CloseWindowButton").GetComponent<Button>();

        this.gameObject.SetActive(false);
    }

    public void closeWindow()
    {
        this.gameObject.SetActive(false);
        planetInfoPanel.openPlanetMenuButton.enabled = true;
    }

    public void updateInformation()
    {
        Planet planetScript = starshipPosition.getTargetObject().gameObject.GetComponent<Planet>();
        this.planetNameText.text = planetScript.gameObject.name;
    }
}
