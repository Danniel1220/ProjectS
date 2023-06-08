using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding
{
    public MiningBuildingType buildingType;
    public string resourceMined; // TODO: implement the mining of different resources
    public int resourceOutput;
    public int populationUsage;
    public int energyUsage;

    public enum MiningBuildingType { Mine, ExcavationFacility };

    public MiningBuilding(MiningBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the resource output depending on what type of mining building this is
        switch (buildingType)
        {
            case MiningBuildingType.Mine:
                resourceOutput = 10;
                populationUsage = 10;
                energyUsage = 1;
                break;
            case MiningBuildingType.ExcavationFacility:
                resourceOutput = 40;
                populationUsage = 1;
                energyUsage = 5;
                break;
        }
    }
}
