using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlanetInfoPanel : MonoBehaviour
{
    private Inventory starshipInventory;
    private StarshipPosition starshipPosition;

    private PlanetMenuPanel planetMenuPanel;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI informationText;
    private TextMeshProUGUI colonyPacksAvailableText;

    private Button colonizeButton;
    public Button openPlanetMenuButton;

    void Start()
    {
        starshipInventory = GameManagers.starshipInventory;
        starshipPosition = GameManagers.starshipPosition;

        planetMenuPanel = UIManagers.planetMenuPanel;

        nameText = this.gameObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        informationText = this.gameObject.transform.Find("InformationText").GetComponent<TextMeshProUGUI>();
        colonyPacksAvailableText = this.gameObject.transform.Find("ColonyPacksCountText").GetComponent<TextMeshProUGUI>();

        colonizeButton = this.gameObject.transform.Find("ColonizeButton").GetComponent<Button>();
        openPlanetMenuButton = this.gameObject.transform.Find("OpenPlanetMenuButton").GetComponent<Button>();

        this.gameObject.SetActive(false);
    }

    public void updateInformation(Planet planetScript)
    {
        nameText.text = planetScript.gameObject.name;
        informationText.text = planetScript.planetInfo;
        colonyPacksAvailableText.text = "(" + starshipInventory.colonyPacks + " Colony Packs available)";

        // planet is colonized so we open the planet info tab accordingly with the planet menu button
        if (planetScript.isColonized) 
        {
            colonizeButton.gameObject.SetActive(false);
            colonyPacksAvailableText.gameObject.SetActive(false);

            openPlanetMenuButton.gameObject.SetActive(true);
        }
        // otherwise open it with the colonization button
        else
        {
            openPlanetMenuButton.gameObject.SetActive(false);

            colonizeButton.gameObject.SetActive(true);
            colonyPacksAvailableText.gameObject.SetActive(true);
        }

    }

    public void attemptToColonize()
    {
        // if we have any colony packs in the inventory
        if (starshipInventory.colonyPacks > 0)
        {
            // get the refference to the planet that is about to be colonized and colonize it
            Planet planetScript = starshipPosition.getTargetObject().GetComponent<Planet>();
            planetScript.colonize();
            // use one colony pack from the inventory
            starshipInventory.useColonyPack();
            // updating the info panel again to reflect the change
            updateInformation(planetScript);
        }
    }

    public void openPlanetMenu()
    {
        planetMenuPanel.gameObject.SetActive(true);
        openPlanetMenuButton.enabled = false;
    }

    public void openWindow()
    {
        this.gameObject.SetActive(true);
    }

    public void closeWindow()
    {
        this.gameObject.SetActive(false);
    }
}