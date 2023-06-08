using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Sector;

public class PlanetMenu : MonoBehaviour
{
    private StarshipPosition starshipPosition;

    private PlanetInfo planetInfoPanel;
    private NewBuildingMenu newBuildingPanel;
    private StoragePanel storagePanel;

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

    private Sector.SectorType selectedSector;

    private float enabledIconAlphaValue = 1f;
    private float disabledIconAlphaValue = 0.2f;

    void Start()
    {
        starshipPosition = GameManagers.starshipPosition;

        planetInfoPanel = UIManagers.planetInfoPanel;
        newBuildingPanel = UIManagers.newBuildingPanel;
        storagePanel = UIManagers.storagePanel;

        planetNameText = this.transform.Find("PlanetNameText").GetComponent<TextMeshProUGUI>();

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
        clearBuildingsGrid();
        this.gameObject.SetActive(false);
        planetInfoPanel.openPlanetMenuButton.enabled = true;

        newBuildingPanel.closePanel();
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
                    habitatSector.isEnabled = true;
                    break;
                case Sector.SectorType.Storage:
                    storageEnabled = true;
                    storageBuildings = sector.storageBuildings;
                    storageProgressBarValue = (float)sector.storageBuildings.Count / sector.maxBuildings;
                    storageSector.setObjectAlpha(enabledIconAlphaValue);
                    storageSector.isEnabled = true;
                    break;
                case Sector.SectorType.Energy:
                    energyEnabled = true;
                    energyBuildings = sector.energyBuildings;
                    energyProgressBarValue = (float)sector.energyBuildings.Count / sector.maxBuildings;
                    energySector.setObjectAlpha(enabledIconAlphaValue);
                    energySector.isEnabled = true;
                    break;
                case Sector.SectorType.Mining:
                    miningEnabled = true;
                    miningBuildings = sector.miningBuildings;
                    miningProgressBarValue = (float)sector.miningBuildings.Count / sector.maxBuildings;
                    miningSector.setObjectAlpha(enabledIconAlphaValue);
                    miningSector.isEnabled = true;
                    break;
                case Sector.SectorType.Production:
                    productionEnabled = true;
                    productionBuildings = sector.productionBuildings;
                    productionProgressBarValue = (float)sector.productionBuildings.Count / sector.maxBuildings;
                    productionSector.setObjectAlpha(enabledIconAlphaValue);
                    productionSector.isEnabled = true;
                    break;
                case Sector.SectorType.Science:
                    scienceEnabled = true;
                    scienceBuildings = sector.scienceBuildings;
                    scienceProgressBarValue = (float)sector.scienceBuildings.Count / sector.maxBuildings;
                    scienceSector.setObjectAlpha(enabledIconAlphaValue);
                    scienceSector.isEnabled = true;
                    break;
            }
        }

        // disable all the icons that weren't present in the sector list
        if (!habitatEnabled) { habitatSector.setObjectAlpha(disabledIconAlphaValue); habitatSector.isEnabled = false; }
        if (!storageEnabled) { storageSector.setObjectAlpha(disabledIconAlphaValue); storageSector.isEnabled = false; }
        if (!energyEnabled) { energySector.setObjectAlpha(disabledIconAlphaValue); energySector.isEnabled = false; }
        if (!miningEnabled) { miningSector.setObjectAlpha(disabledIconAlphaValue); miningSector.isEnabled = false; }
        if (!productionEnabled) { productionSector.setObjectAlpha(disabledIconAlphaValue); productionSector.isEnabled = false; }
        if (!scienceEnabled) { scienceSector.setObjectAlpha(disabledIconAlphaValue); scienceSector.isEnabled = false; }

        // update all progress bar values
        habitatSector.setProgressBarValue(habitatProgressBarValue);
        storageSector.setProgressBarValue(storageProgressBarValue);
        energySector.setProgressBarValue(energyProgressBarValue);
        miningSector.setProgressBarValue(miningProgressBarValue);
        productionSector.setProgressBarValue(productionProgressBarValue);
        scienceSector.setProgressBarValue(scienceProgressBarValue);

        planetScript.updatePlanetStats();
        updateStorageUI();
    }

    public void displayHabitatBuildings()
    {
        foreach(HabitatBuilding building in habitatBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Habitat;
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayStorageBuildings()
    {
        foreach (StorageBuilding building in storageBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Storage;
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayEnergyBuildings()
    {
        foreach (EnergyBuilding building in energyBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Energy;
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayMiningBuildings()
    {
        foreach (MiningBuilding building in miningBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Mining;
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayProductionBuildings()
    {
        foreach (ProductionBuilding building in productionBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Production;
        addNewBuildingIconToBuildingsGrid();
    }

    public void displayScienceBuildings()
    {
        foreach (ScienceBuilding building in scienceBuildings)
        {
            addBuildingToBuildingsGrid(building.buildingType.ToString());
        }
        selectedSector = Sector.SectorType.Science;
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
        // fetching the target object to see if there are any free slots for that sector
        // if there aren't, just dont show the button
        Planet targetPlanetScript = starshipPosition.getTargetObject().GetComponent<Planet>();
        bool shouldCreateButton = false;

        // iterate through each sector, find the currently selected sector and
        // check if it has less buildings than the max, if yes, we should create a new building button
        foreach (Sector sector in targetPlanetScript.sectors)
        {
            if (sector.type == selectedSector && 
                sector.type == Sector.SectorType.Habitat &&
                sector.habitatBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
            if (sector.type == selectedSector &&
                sector.type == Sector.SectorType.Storage &&
                sector.storageBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
            if (sector.type == selectedSector &&
                sector.type == Sector.SectorType.Energy &&
                sector.energyBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
            if (sector.type == selectedSector &&
                sector.type == Sector.SectorType.Mining &&
                sector.miningBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
            if (sector.type == selectedSector &&
                sector.type == Sector.SectorType.Production &&
                sector.productionBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
            if (sector.type == selectedSector &&
                sector.type == Sector.SectorType.Science &&
                sector.scienceBuildings.Count < sector.maxBuildings)
            {
                shouldCreateButton = true;
                break;
            }
        }
        if (shouldCreateButton)
        {
            GameObject newBuildingButton = Instantiate(addBuildingIconPrefab);
            newBuildingButton.transform.SetParent(buildingsGrid.transform, false);
            newBuildingButton.GetComponent<Button>().onClick.AddListener(() => { newBuildingButtonOnClick(); });

        }
    }

    public void clearBuildingsGrid()
    {
        // this function clears all the children inside the buildings grid to make room for new ones
        foreach (Transform child in buildingsGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void newBuildingButtonOnClick()
    {
        newBuildingPanel.openPanel(selectedSector);
    }

    public void addBuildingToPlanet(Sector.SectorType sectorType, string buildingName)
    {
        Planet targetPlanetScript = starshipPosition.getTargetObject().GetComponent<Planet>();
        if (targetPlanetScript != null)
        {
            targetPlanetScript.addBuilding(sectorType, buildingName);
        }
        else
        {
            Debug.LogError("Failed to get Planet component from object " + starshipPosition.getTargetObject() +
                "\nThis probably means the target object wasnt a planet" +
                "\nTarget name: " + starshipPosition.getTargetObject().name);
        }
        
    }

    public void addSectorToPlanet(string sectorName)
    {
        Planet targetPlanetScript = starshipPosition.getTargetObject().GetComponent<Planet>();
        if (targetPlanetScript != null)
        {
            SectorType sectorType;
            if (Enum.TryParse(sectorName, out sectorType))
            {
                targetPlanetScript.addSector(sectorType);
                selectedSector = sectorType;
                updateInformation();
                clearBuildingsGrid();
                switch (sectorType)
                {
                    case Sector.SectorType.Habitat:
                        displayHabitatBuildings();
                        break;
                    case Sector.SectorType.Storage:
                        displayEnergyBuildings();
                        break;
                    case Sector.SectorType.Energy:
                        displayEnergyBuildings();
                        break;
                    case Sector.SectorType.Mining:
                        displayMiningBuildings();
                        break;
                    case Sector.SectorType.Production:
                        displayProductionBuildings();
                        break;
                    case Sector.SectorType.Science:
                        displayScienceBuildings();
                        break;
                }
            }
            else
            {
                Debug.LogError("Error while trying to parse sector type with input: " + sectorName);
            }
        }
        else
        {
            Debug.LogError("Failed to get Planet component from object " + starshipPosition.getTargetObject() +
                "\nThis probably means the target object wasnt a planet" +
                "\nTarget name: " + starshipPosition.getTargetObject().name);
        }
    }

    public void updateStorageUI()
    {
        Planet planetScript = starshipPosition.getTargetObject().gameObject.GetComponent<Planet>();
        storagePanel.clearItems();
        foreach(Tuple<Item, int> item in planetScript.storageList)
        {
            storagePanel.addItem(item.Item1, item.Item2);
        }
    }
}
