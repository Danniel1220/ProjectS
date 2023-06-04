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

    private GameObject buildingsGridList;

    private Button closeWindowButton;

    private GameObject buildingIconPrefab;

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
        buildingsGridList = this.transform.Find("Buildings").Find("Scroll View").Find("Viewport").Find("Content").gameObject;

        buildingIconPrefab = Resources.Load("Prefabs/BuildingIcon") as GameObject;

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
                    habitatProgressBarValue = sector.habitatBuildings.Count / sector.maxBuildings;
                    habitatSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Storage:
                    storageEnabled = true;
                    storageBuildings = sector.storageBuildings;
                    storageProgressBarValue = sector.storageBuildings.Count / sector.maxBuildings;
                    storageSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Energy:
                    energyEnabled = true;
                    energyBuildings = sector.energyBuildings;
                    energyProgressBarValue = sector.energyBuildings.Count / sector.maxBuildings;
                    energySector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Mining:
                    miningEnabled = true;
                    miningBuildings = sector.miningBuildings;
                    miningProgressBarValue = sector.miningBuildings.Count / sector.maxBuildings;
                    miningSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Production:
                    productionEnabled = true;
                    productionBuildings = sector.productionBuildings;
                    productionProgressBarValue = sector.productionBuildings.Count / sector.maxBuildings;
                    productionSector.setObjectAlpha(enabledIconAlphaValue);
                    break;
                case Sector.SectorType.Science:
                    scienceEnabled = true;
                    scienceBuildings = sector.scienceBuildings;
                    scienceProgressBarValue = sector.scienceBuildings.Count / sector.maxBuildings;
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
            GameObject buildingIcon = Instantiate(buildingIconPrefab);
            // worldPositionStays argument set to false
            // this will retain local orientation and scale rather than world orientation and scale
            // which is what i want for an UI element
            buildingIcon.transform.SetParent(buildingsGridList.transform, false);
            BuildingIcon buildingIconScript = buildingIcon.AddComponent<BuildingIcon>();
            buildingIconScript.init();
            // text.text because the first text is a refference to the TMP text object named text
            // and the second one is the text field inside the TMP text object
            // also, this regex will add spaces inbetween words because the enum name doesnt have any obviously
            buildingIconScript.text.text = Regex.Replace(building.buildingType.ToString(), "([a-z])([A-Z])", "$1 $2");
            //buildingIconScript.background.color = Color.green;
        }
        // creating another building icon on top of the ones in the sector that will contain
        // a plus sign which will be used to add buildings
        GameObject addBuildingIcon = Instantiate(buildingIconPrefab);
        addBuildingIcon.transform.SetParent(buildingsGridList.transform, false);
        BuildingIcon addBuildingIconScript = addBuildingIcon.AddComponent<BuildingIcon>();
        addBuildingIconScript.init();
        addBuildingIconScript.text.text = "+";
        addBuildingIconScript.text.fontSize = 24;
        addBuildingIconScript.background.color = Color.green;
    }

    public void displayStorageBuildings()
    {

    }

    public void displayEnergyBuildings()
    {

    }

    public void displayMiningBuildings()
    {

    }

    public void displayProductionBuildings()
    {

    }

    public void displayScienceBuildings()
    {

    }
}
