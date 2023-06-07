using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using static Sector;

public class NewBuildingMenu : MonoBehaviour
{
    private PlanetMenu planetMenuPanel;
    private GameObject buildingsGrid;
    private GameObject buildingGridButton;

    private Transform buildingButtonSelected;

    private Sector.SectorType sectorTypeDisplayed;

    void Start()
    {
        planetMenuPanel = UIManagers.planetMenuPanel;
        buildingsGrid = this.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        buildingGridButton = Resources.Load("Prefabs/NewBuildingGridButton") as GameObject;

        this.gameObject.SetActive(false);
    }

    public void addBuildingButtonToGrid(string buildingName, Color normalColor, Color highlightedColor, Color pressedColor, Color selectedColor, Color disabledColor)
    {
        GameObject buildingButton = Instantiate(buildingGridButton);
        buildingButton.transform.SetParent(buildingsGrid.transform, false);
        buildingButton.name = buildingName;
        buildingButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Regex.Replace(buildingName, "([a-z])([A-Z])", "$1 $2");
        buildingButton.GetComponent<Button>().onClick.AddListener(() => { buildingIconClicked(buildingName); });
    }

    public void buildingIconClicked(string buildingName)
    {
        string trimmedBuildingName = buildingName.Replace(" ", "");

        // if there is any button currently highlighted then remove the highlight since another button is about to get it
        // unless the button is the same... in which case, too bad!
        if (buildingButtonSelected != null)
        {
            Destroy(buildingButtonSelected.transform.GetComponent<Outline>());
        }

        foreach(Transform button in buildingsGrid.transform)
        {
            // if it's the right button, highlight it and save the refference
            if (button.name == trimmedBuildingName)
            {
                buildingButtonSelected = button;
                highlightButton(button);
            }
        }
    }

    private void highlightButton(Transform button)
    {
        Outline buttonHighlight = button.gameObject.AddComponent<Outline>();
        buttonHighlight.effectColor = Color.white;
        buttonHighlight.effectDistance = new Vector2(4f, 4f);
    }

    public void buildSelectedBuilding()
    {
        // if there is any building selected
        if (buildingButtonSelected != null)
        {
            // addings the building to the planet script, closing the new building menu, updating the info in planet menu
            // and also updating the buildings grid by clearing it and then displaying the currently selected sector's buildings
            planetMenuPanel.addBuildingToPlanet(sectorTypeDisplayed, buildingButtonSelected.name);
            closePanel();
            planetMenuPanel.updateInformation();
            planetMenuPanel.clearBuildingsGrid();
            switch (sectorTypeDisplayed)
            {
                case Sector.SectorType.Habitat:
                    planetMenuPanel.displayHabitatBuildings();
                    break;
                case Sector.SectorType.Storage:
                    planetMenuPanel.displayStorageBuildings();
                    break;
                case Sector.SectorType.Energy:
                    planetMenuPanel.displayEnergyBuildings();
                    break;
                case Sector.SectorType.Mining:
                    planetMenuPanel.displayMiningBuildings();
                    break;
                case Sector.SectorType.Production:
                    planetMenuPanel.displayProductionBuildings();
                    break;
                case Sector.SectorType.Science:
                    planetMenuPanel.displayScienceBuildings();
                    break;
            }
        }
        else
        {
            // TODO: notify the player no building is selected
        }
    }

    public void openPanel(Sector.SectorType sectorType)
    {
        buildingButtonSelected = null;
        string[] habitatBuildingTypeNames;

        switch (sectorType)
        {
            case Sector.SectorType.Habitat:
                sectorTypeDisplayed = Sector.SectorType.Habitat;
                habitatBuildingTypeNames = Enum.GetNames(typeof(HabitatBuilding.HabitatBuildingType));
                for(int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
            case Sector.SectorType.Storage:
                sectorTypeDisplayed = Sector.SectorType.Storage;
                habitatBuildingTypeNames = Enum.GetNames(typeof(StorageBuilding.StorageBuildingType));
                for (int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
            case Sector.SectorType.Energy:
                sectorTypeDisplayed = Sector.SectorType.Energy;
                habitatBuildingTypeNames = Enum.GetNames(typeof(EnergyBuilding.EnergyBuildingType));
                for (int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
            case Sector.SectorType.Mining:
                sectorTypeDisplayed = Sector.SectorType.Mining;
                habitatBuildingTypeNames = Enum.GetNames(typeof(MiningBuilding.MiningBuildingType));
                for (int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
            case Sector.SectorType.Production:
                sectorTypeDisplayed = Sector.SectorType.Production;
                habitatBuildingTypeNames = Enum.GetNames(typeof(ProductionBuilding.ProductionBuildingType));
                for (int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
            case Sector.SectorType.Science:
                sectorTypeDisplayed = Sector.SectorType.Science;
                habitatBuildingTypeNames = Enum.GetNames(typeof(ScienceBuilding.ScienceBuildingType));
                for (int i = 0; i < habitatBuildingTypeNames.Length; i++)
                {
                    addBuildingButtonToGrid(
                        habitatBuildingTypeNames[i],
                        new Color(0, 207, 34),
                        new Color(0, 173, 38),
                        new Color(0, 135, 30),
                        new Color(0, 89, 20),
                        new Color(0, 0, 0, 128));
                }
                break;
        }

        this.gameObject.SetActive(true);
    }

    public void closePanel()
    {
        // delete all buttons from buildingsGrid so the grid is empty when re-opening the panel (when buttons are added)
        foreach(Transform child in buildingsGrid.transform)
        {
            Destroy(child.gameObject);
        }

        this.gameObject.SetActive(false);
    }
}
