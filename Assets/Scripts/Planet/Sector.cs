using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{
    public SectorType type;
    public int currentBuildings;
    public int maxBuildings;
    public List<HabitatBuilding> habitatBuildings;
    public List<StorageBuilding> storageBuildings;
    public List<EnergyBuilding> energyBuildings;
    public List<MiningBuilding> miningBuildings;
    public List<ProductionBuilding> productionBuildings;
    public List<ScienceBuilding> scienceBuildings;

    public enum SectorType { Habitat, Storage, Energy, Mining, Production, Science};

    public Sector(SectorType type)
    {
        this.type = type;
        maxBuildings = 4;
        switch (type)
        {
            case SectorType.Habitat:
                habitatBuildings = new List<HabitatBuilding>();
                // every colonized planet will start out with a small settlement of 2 people
                habitatBuildings.Add(new HabitatBuilding(HabitatBuilding.HabitatBuildingType.SmallSettlement));
                break;
            case SectorType.Storage:
                storageBuildings = new List<StorageBuilding>();
                break;
            case SectorType.Energy:
                energyBuildings = new List<EnergyBuilding>();
                break;
            case SectorType.Mining:
                storageBuildings = new List<StorageBuilding>();
                break;
            case SectorType.Production:
                energyBuildings = new List<EnergyBuilding>();
                break;
            case SectorType.Science:
                storageBuildings = new List<StorageBuilding>();
                break;
        }
    }

    public void addHabitatBuilding(HabitatBuilding.HabitatBuildingType buildingType)
    {
        habitatBuildings.Add(new HabitatBuilding(buildingType));
    }

    public void addStorageBuilding(StorageBuilding.StorageBuildingType buildingType)
    {
        storageBuildings.Add(new StorageBuilding(buildingType));
    }

    public void addEnergyBuilding(EnergyBuilding.EnergyBuildingType buildingType)
    {
        energyBuildings.Add(new EnergyBuilding(buildingType));
    }

    public void addMiningBuilding(MiningBuilding.MiningBuildingType buildingType)
    {
        miningBuildings.Add(new MiningBuilding(buildingType));
    }

    public void addProductionBuilding(ProductionBuilding.ProductionBuildingType buildingType)
    {
        productionBuildings.Add(new ProductionBuilding(buildingType));
    }

    public void addScienceBuilding(ScienceBuilding.ScienceBuildingType buildingType)
    {
        scienceBuildings.Add(new ScienceBuilding(buildingType));
    }
}
