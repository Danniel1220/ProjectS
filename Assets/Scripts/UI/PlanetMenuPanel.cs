using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenuPanel : MonoBehaviour
{
    private StarshipPosition starshipPosition;

    private PlanetInfoPanel planetInfoPanel;

    private TextMeshProUGUI planetNameText;

    private SectorIcon habitatSector;
    private SectorIcon storageSector;
    private SectorIcon energySector;
    private SectorIcon miningSector;
    private SectorIcon productionSector;
    private SectorIcon scienceSector;

    private List<HabitatBuilding> habitatBuildings;
    private List<StorageBuilding> storageBuildings;
    private List<EnergyBuilding> energyBuildings;
    private List<MiningBuilding> miningBuildings;
    private List<ProductionBuilding> productionBuildings;
    private List<ScienceBuilding> scienceBuildings;

    private GameObject buildingsGrid;

    private Button closeWindowButton;

    private GameObject buildingIconPrefab;
    private GameObject addBuildingIconPrefab;

    private bool habitatEnabled;
    private bool storageEnabled;
    private bool energyEnabled;
    private bool miningEnabled;
    private bool productionEnabled;
    private bool scienceEnabled;

    private float habitatProgressBarValue;
    private float storageProgressBarValue;
    private float energyProgressBarValue;
    private float miningProgressBarValue;
    private float productionProgressBarValue;
    private float scienceProgressBarValue;

    private float enabledIconAlphaValue = 1f;
    private float disabledIconAlphaValue = 0.2f;

    void Start()
    {
        starshipPosition = GameManagers.starshipPosition;

        planetInfoPanel = UIManagers.planetInfoPanel;

        planetNameText = this.transform.Find("PlanetNameText").GetComponent<TextMeshProUGUI>();
        closeWindowButton = this.transform.Find("CloseWindowButton").GetComponent<Button>();

        // sector icon parent game objects
        habitatSector = this.transform.Find("Sectors").Find("Habitat").GetComponent<SectorIcon>();
        storageSector = this.transform.Find("Sectors").Find("Storage").GetComponent<SectorIcon>();
        energySector = this.transform.Find("Sectors").Find("Energy").GetComponent<SectorIcon>();
        miningSector = this.transform.Find("Sectors").Find("Mining").GetComponent<SectorIcon>();
        productionSector = this.transform.Find("Sectors").Find("Production").GetComponent<SectorIcon>();
        scienceSector = this.transform.Find("Sectors").Find("Science").GetComponent<SectorIcon>();

        // this is the gameobject where i have to place the building icons
        buildingsGrid = this.transform.Find("Buildings").Find("Scroll View").Find("Viewport").Find("Content").gameObject;

        buildingIconPrefab = Resources.Load("Prefabs/BuildingIcon") as GameObject;
        addBuildingIconPrefab = Resources.Load("Prefabs/NewBuildingIcon") as GameObject;

        // disable the panel once we cache all the required refferences
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

        // assume that the planet has no sectors
        habitatEnabled = false;
        storageEnabled = false;
        energyEnabled = false;
        miningEnabled = false;
        productionEnabled = false;
        scienceEnabled = false;

        // default values for progress bars are 0
        habitatProgressBarValue = 0;
        storageProgressBarValue = 0;
        energyProgressBarValue = 0;
        miningProgressBarValue = 0;
        productionProgressBarValue = 0;
        scienceProgressBarValue = 0;

        // iterate through all the sectors that exist and assign the relevant data
        List<Sector> sectors = planetScript.getSectorList();
        foreach (Sector sector in sectors)
        {
            switch (sector.type)
            {
                case Sector.SectorType.Habitat:
                    habitatEnabled = true;
                    habitatBuildings = sector.habitatBuildings;
                    habitatProgressBarValue = (float)sector.habitatBuildings.Count / sector.maxBuildings;
                    habitatSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Storage:
                    storageEnabled = true;
                    storageBuildings = sector.storageBuildings;
                    storageProgressBarValue = (float)sector.storageBuildings.Count / sector.maxBuildings;
                    storageSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Energy:
                    energyEnabled = true;
                    energyBuildings = sector.energyBuildings;
                    energyProgressBarValue = (float)sector.energyBuildings.Count / sector.maxBuildings;
                    energySector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Mining:
                    miningEnabled = true;
                    miningBuildings = sector.miningBuildings;
                    miningProgressBarValue = (float)sector.miningBuildings.Count / sector.maxBuildings;
                    miningSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Production:
                    productionEnabled = true;
                    productionBuildings = sector.productionBuildings;
                    productionProgressBarValue = (float)sector.productionBuildings.Count / sector.maxBuildings;
                    productionSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Science:
                    scienceEnabled = true;
                    scienceBuildings = sector.scienceBuildings;
                    scienceProgressBarValue = (float)sector.scienceBuildings.Count / sector.maxBuildings;
                    scienceSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
            }
        }

        // disable all the icons that weren't present in the sector list
        if (!habitatEnabled) habitatSector.setObjectAlpha(disabledIconAlphaValue);
        if (!storageEnabled) storageSector.setObjectAlpha(disabledIconAlphaValue);
        if (!energyEnabled) energySector.setObjectAlpha(disabledIconAlphaValue);
        if (!miningEnabled) miningSector.setObjectAlpha(disabledIconAlphaValue);
        if (!productionEnabled) productionSector.setObjectAlpha(disabledIconAlphaValue);
        if (!scienceEnabled) scienceSector.setObjectAlpha(disabledIconAlphaValue);

        // update all progress bar values
        habitatSector.setProgressBarValue(habitatProgressBarValue);
        storageSector.setProgressBarValue(storageProgressBarValue);
        energySector.setProgressBarValue(habitatProgressBarValue);
        miningSector.setProgressBarValue(habitatProgressBarValue);
        productionSector.setProgressBarValue(habitatProgressBarValue);
        scienceSector.setProgressBarValue(habitatProgressBarValue);
    }

    public void displayHabitatBuildings()
    {
        foreach(HabitatBuilding building in habitatBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayStorageBuildings()
    {
        foreach (StorageBuilding building in storageBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayEnergyBuildings()
    {
        foreach (EnergyBuilding building in energyBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayMiningBuildings()
    {
        foreach (MiningBuilding building in miningBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayProductionBuildings()
    {
        foreach (ProductionBuilding building in productionBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayScienceBuildings()
    {
        foreach (ScienceBuilding building in scienceBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        addNewBuildingIconToBuildingsGrid();
    }

    private void addBuildingToBuildingsGrid(string buildingName)
    {
        GameObject buildingIcon = Instantiate(buildingIconPrefab);
        buildingIcon.transform.SetParent(buildingsGrid.transform, false);
        BuildingIcon buildingIconScript = buildingIcon.AddComponent<BuildingIcon>();
        buildingIconScript.init();
        buildingIconScript.text.text = Regex.Replace(buildingName, "([a-z])([A-Z])", "$1 $2");
    }

    public void addNewBuildingIconToBuildingsGrid()
    {
        GameObject newBuildingIcon = Instantiate(addBuildingIconPrefab);
        newBuildingIcon.transform.SetParent(buildingsGrid.transform, false);
    }

    public void clearBuildingsGrid()
    {
        // this function clears all the children inside the buildings grid to make room for new ones
        foreach (Transform child in buildingsGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
