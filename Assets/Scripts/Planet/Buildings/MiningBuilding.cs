using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding
{
    public MiningBuildingType buildingType;
    public string resourceMined;
    public int resourceOutput;

    public enum MiningBuildingType { Mine, ExcavationFacility };

    public MiningBuilding(MiningBuildingType buildingType, string resourceMined)
    {
        this.buildingType = buildingType;
        this.resourceMined = resourceMined;

        // assigning the resource output depending on what type of mining building this is
        switch (buildingType)
        {
            case MiningBuildingType.Mine:
                resourceOutput = 10;
                break;
            case MiningBuildingType.ExcavationFacility:
                resourceOutput = 20;
                break;
        }

        this.resourceMined = resourceMined;
    }
}
