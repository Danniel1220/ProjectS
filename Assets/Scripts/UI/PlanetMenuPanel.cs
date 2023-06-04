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

    private SectorIcon habitatSector;
    private SectorIcon storageSector;
    private SectorIcon energySector;
    private SectorIcon miningSector;
    private SectorIcon productionSector;
    private SectorIcon scienceSector;

    private Button closeWindowButton;

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
        habitatSector = this.transform.Find("Habitat").GetComponent<SectorIcon>();
        storageSector = this.transform.Find("Storage").GetComponent<SectorIcon>();
        energySector = this.transform.Find("Energy").GetComponent<SectorIcon>();
        miningSector = this.transform.Find("Mining").GetComponent<SectorIcon>();
        productionSector = this.transform.Find("Production").GetComponent<SectorIcon>();
        scienceSector = this.transform.Find("Science").GetComponent<SectorIcon>();

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

        // iterate through all the sectors that exist and assign the relevant data
        List<Sector> sectors = planetScript.getSectorList();
        foreach (Sector sector in sectors)
        {
            switch (sector.type)
            {
                case Sector.SectorType.Habitat:
                    habitatEnabled = true;
                    habitatProgressBarValue = sector.habitatBuildings.Count / sector.maxBuildings;
                    break;
                case Sector.SectorType.Storage:
                    storageEnabled = true;
                    storageProgressBarValue = sector.storageBuildings.Count / sector.maxBuildings;
                    break;
                case Sector.SectorType.Energy:
                    energyEnabled = true;
                    energyProgressBarValue = sector.energyBuildings.Count / sector.maxBuildings;
                    break;
                case Sector.SectorType.Mining:
                    miningEnabled = true;
                    miningProgressBarValue = sector.miningBuildings.Count / sector.maxBuildings;
                    break;
                case Sector.SectorType.Production:
                    productionEnabled = true;
                    productionProgressBarValue = sector.productionBuildings.Count / sector.maxBuildings;
                    break;
                case Sector.SectorType.Science:
                    scienceEnabled = true;
                    scienceProgressBarValue = sector.scienceBuildings.Count / sector.maxBuildings;
                    break;
            }
        }


        // enable or disable each icon depending on whether the sector exists on the planet or not
        if (habitatEnabled) habitatSector.setObjectAlpha(enabledIconAlphaValue);
        else habitatSector.setObjectAlpha(0);

        if (storageEnabled) storageSector.setObjectAlpha(enabledIconAlphaValue);
        else storageSector.setObjectAlpha(0);

        if (energyEnabled) energySector.setObjectAlpha(enabledIconAlphaValue);
        else energySector.setObjectAlpha(0);

        if (miningEnabled) miningSector.setObjectAlpha(enabledIconAlphaValue);
        else miningSector.setObjectAlpha(0);

        if (productionEnabled) productionSector.setObjectAlpha(enabledIconAlphaValue);
        else productionSector.setObjectAlpha(0);

        if (scienceEnabled) scienceSector.setObjectAlpha(enabledIconAlphaValue);
        else scienceSector.setObjectAlpha(0);
    }
}
