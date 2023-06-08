using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    private SectorsStats sectorsStats;
    private PlanetMenu planetMenu;

    public string planetName;

    public int index;
    public List<Resource> resources;
    public string planetInfo;
    public bool isColonized;

    public bool hasHabitatSector;
    public bool hasStorageSector;
    public bool hasEnergySector;
    public bool hasMiningSector;
    public bool hasProductionSector;
    public bool hasScienceSector;

    public List<Sector> sectors;

    public int population;
    public int populationUsage;
    public int storageSlots;
    public int storageUsage;
    public int energyOutput;
    public int energyUsage;
    public int miningOutput;
    public int researchSpeed;

    public List<Tuple<Item, int>> storageList;

    public void init()
    {
        initResources();
        initPlanetInfo();
        sectors = new List<Sector>();
        storageList = new List<Tuple<Item, int>>();
        planetMenu = UIManagers.planetMenuPanel;
    }

    public void updatePlanetStats()
    {
        populationUsage = 0;
        energyUsage = 0;
        sectorsStats = UIManagers.sectorStats;
        foreach(Sector sector in sectors)
        {
            if (sector.type == Sector.SectorType.Habitat)
            {
                population = 0;
                foreach(HabitatBuilding habitatBuilding in sector.habitatBuildings)
                {
                    population += habitatBuilding.population;
                    energyUsage += habitatBuilding.energyUsage;
                }
            }
            if (sector.type == Sector.SectorType.Storage)
            {
                storageSlots = 0;
                foreach(StorageBuilding storageBuilding in sector.storageBuildings)
                {
                    storageSlots += storageBuilding.storageSlots;
                }
            }
            if (sector.type == Sector.SectorType.Energy)
            {
                energyOutput = 0;
                foreach(EnergyBuilding energyBuilding in sector.energyBuildings)
                {
                    energyOutput += energyBuilding.energyOutput;
                }
            }
            if (sector.type == Sector.SectorType.Mining)
            {
                miningOutput = 0;
                foreach(MiningBuilding miningBuilding in sector.miningBuildings)
                {
                    miningOutput += miningBuilding.resourceOutput;
                    populationUsage += miningBuilding.populationUsage;
                    energyUsage += miningBuilding.energyUsage;
                }
            }
            if (sector.type == Sector.SectorType.Production)
            {
                foreach(ProductionBuilding productionBuilding in sector.productionBuildings)
                {
                    populationUsage += productionBuilding.populationUsage;
                    energyUsage += productionBuilding.energyUsage;
                }
            }
            if (sector.type == Sector.SectorType.Science)
            {
                researchSpeed = 0;
                foreach(ScienceBuilding scienceBuilding in sector.scienceBuildings)
                {
                    researchSpeed += scienceBuilding.researchSpeed;
                    populationUsage += scienceBuilding.populationUsage;
                    energyUsage += scienceBuilding.energyUsage;
                }
            }
        }

        sectorsStats.setStats(population, storageSlots, energyOutput, miningOutput, 0, researchSpeed);
    }

    public void addSector(Sector.SectorType sectorType)
    {
        sectors.Add(new Sector(sectorType));
        switch (sectorType)
        {
            case Sector.SectorType.Habitat:
                hasHabitatSector = true;
                break;
            case Sector.SectorType.Storage:
                hasStorageSector = true;
                break;
            case Sector.SectorType.Energy:
                hasEnergySector = true;
                break;
            case Sector.SectorType.Mining:
                hasMiningSector = true;
                break;
            case Sector.SectorType.Production:
                hasProductionSector = true;
                break;
            case Sector.SectorType.Science:
                hasScienceSector = true;
                break;
        }
    }

    public void addBuilding(Sector.SectorType sectorType, string buildingName)
    {
        switch (sectorType)
        {
            case Sector.SectorType.Habitat:
                {
                    HabitatBuilding.HabitatBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addHabitatBuilding(buildingType);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to HabitatBuildingType failed...");
                    }
                    break;
                }

            case Sector.SectorType.Storage:
                {
                    StorageBuilding.StorageBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addStorageBuilding(buildingType);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to StorageBuildingType failed...");
                    }
                    break;
                }
            case Sector.SectorType.Energy:
                {
                    EnergyBuilding.EnergyBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addEnergyBuilding(buildingType);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to EnergyBuildingType failed...");
                    }
                    break;
                }
            case Sector.SectorType.Mining:
                {
                    MiningBuilding.MiningBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addMiningBuilding(buildingType);
                                InvokeRepeating("mineResources", 0f, 1f);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to MiningBuildingType failed...");
                    }
                    break;
                }
            case Sector.SectorType.Production:
                {
                    ProductionBuilding.ProductionBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addProductionBuilding(buildingType);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to ProductionBuildingType failed...");
                    }
                    break;
                }
            case Sector.SectorType.Science:
                {
                    ScienceBuilding.ScienceBuildingType buildingType;
                    if (Enum.TryParse(buildingName, out buildingType))
                    {
                        foreach (Sector sector in sectors)
                        {
                            if (sector.type == sectorType)
                            {
                                sector.addScienceBuilding(buildingType);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Enum parsing of string " + buildingName + " to ScienceBuildingType failed...");
                    }
                    break;
                }
        }
    }

    private void initPlanetInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Resource resource in resources)
        {
            sb.Append(resource.name);
            sb.Append(" (");
            sb.Append(resource.percentageByPlanetWeight * 100);
            sb.Append("%)\n");
        }
        planetInfo = sb.ToString();
    }

    private void initResources()
    {
        List<Resource> resources = new List<Resource>();

        float ironMin = 0.3f;
        float ironMax = 0.35f;
        float iron = Mathf.Round(Random.Range(ironMin, ironMax) * 10000) / 10000f;

        float oxygenMin = 0.28f;
        float oxygenMax = 0.32f;
        float oxygen = Mathf.Round(Random.Range(oxygenMin, oxygenMax) * 10000) / 10000;

        float silliconMin = 0.148f;
        float silliconMax = 0.152f;
        float sillicon = Mathf.Round(Random.Range(silliconMin, silliconMax) * 10000) / 10000;

        float magnesiumMin = 0.138f;
        float magnesiumMax = 0.142f;
        float magnesium = Mathf.Round(Random.Range(magnesiumMin, magnesiumMax) * 10000) / 10000;

        float sulfurMin = 0.026f;
        float sulfurMax = 0.032f;
        float sulfur = Mathf.Round(Random.Range(sulfurMin, sulfurMax) * 10000) / 10000;

        float nickelMin = 0.014f;
        float nickelMax = 0.020f;
        float nickel = Mathf.Round(Random.Range(nickelMin, nickelMax) * 10000) / 10000;

        float calciumMin = 0.012f;
        float calciumMax = 0.016f;
        float calcium = Mathf.Round(Random.Range(calciumMin, calciumMax) * 10000) / 10000;

        float aluminumMin = 0.012f;
        float aluminumMax = 0.016f;
        float aluminum = Mathf.Round(Random.Range(aluminumMin, aluminumMax) * 10000) / 10000;

        float traceMin = 0.010f;
        float traceMax = 0.014f;
        float trace = Mathf.Round(Random.Range(traceMin, traceMax) * 10000) / 10000;

        resources.Add(new Resource("Iron", "Fe", iron));
        resources.Add(new Resource("Oxygen", "O", oxygen));
        resources.Add(new Resource("Sillicon", "Si", sillicon));
        resources.Add(new Resource("Magnesium", "Mg", magnesium));
        resources.Add(new Resource("Sulfur", "S", sulfur));
        resources.Add(new Resource("Nickel", "Ni", nickel));
        resources.Add(new Resource("Calcium", "Fe", calcium));
        resources.Add(new Resource("Aluminum", "Fe", aluminum));
        resources.Add(new Resource("Trace Elements", "Trace", trace));

        this.resources = resources;
    }

    public void colonize()
    {
        isColonized = true;

        // make the trail green for colonized planets
        this.gameObject.GetComponent<Trail>().setTrailColor(Color.green);

        // all the planets, by default, will have a habitat and storage sector created when they are colonized
        addSector(Sector.SectorType.Habitat);
        addSector(Sector.SectorType.Storage);
    }

    // this function is used to retreive all the relevant data for the planet menu panel
    public List<Sector> getSectorList()
    {
        return sectors;
    }

    public void addItemToStorage(int id, int amount)
    {
        bool itemAlreadyInStorage = false;
        // first check if that type of item is already in the storage
        // (the reason why is use a for and not a foreach as usual is because i have to reasign the tuple
        // since it is immutable, and im not allowed to assign the item i loop over in a foreach)
        for (int i = 0; i < storageList.Count; i++)
        {
            // found the same item in storage
            if (storageList[i].Item1.id == id)
            {
                itemAlreadyInStorage = true;
                storageList[i] = Tuple.Create(storageList[i].Item1, storageList[i].Item2 + amount);
            }
        }

        // if the item isnt already in storage we have to create a new one
        if (!itemAlreadyInStorage)
        {
            storageList.Add(Tuple.Create(ItemObject.getItemById(id), amount));
        }
    }

    public void takeItemFromStorage(Item item, int amount)
    {
        
    }

    public void mineResources()
    {
        int resourcesMined = 0;

        foreach(Sector sector in sectors)
        {
            if (sector.type == Sector.SectorType.Mining)
            {
                foreach(MiningBuilding miningBuilding in sector.miningBuildings)
                {
                    resourcesMined += miningBuilding.resourceOutput;
                }
                break;
            }

        }
        addItemToStorage(1, resourcesMined);
        if (planetMenu.isActiveAndEnabled) planetMenu.updateStorageUI();
        Debug.Log("Mined " + resourcesMined + " on planet " + planetName);
    }
}
