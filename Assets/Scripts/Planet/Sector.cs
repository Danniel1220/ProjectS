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
                habitatBuildings.Add(new HabitatBuilding(HabitatBuilding.HabitatBuildingType.SmallSettlement, 2));
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
}
