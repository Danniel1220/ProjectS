using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{
    public SectorType type;
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
        
        switch (type)
        {
            case SectorType.Habitat:
                habitatBuildings = new List<HabitatBuilding>();
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
