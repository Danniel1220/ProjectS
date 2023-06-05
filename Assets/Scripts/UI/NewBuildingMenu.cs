using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBuildingMenu : MonoBehaviour
{
    private PlanetMenu planetMenuPanel;
    private GameObject buildingsGrid;
    private GameObject buildingGridButton;

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
        Debug.Log(buildingName);
    }

    public void openPanel(Sector.SectorType sectorType)
    {
        switch(sectorType)
        {
            case Sector.SectorType.Habitat:
                string[] habitatBuildingTypeNames = Enum.GetNames(typeof(HabitatBuilding.HabitatBuildingType));
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

                break;
            case Sector.SectorType.Energy:

                break;
            case Sector.SectorType.Mining:

                break;
            case Sector.SectorType.Production:

                break;
            case Sector.SectorType.Science:

                break;
        }

        this.gameObject.SetActive(true);
    }

    public void closePanel()
    {
        // delete all buttons from buildingsGrid so the grid is empty when re-opening the panel (when buttons are added)
        foreach(Transform child in buildingsGrid.transform)
        {
            Destroy(child);
        }

        this.gameObject.SetActive(false);
    }
}
