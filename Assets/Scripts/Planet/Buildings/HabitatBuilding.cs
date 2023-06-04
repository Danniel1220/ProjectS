using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatBuilding
{
    public HabitatBuildingType buildingType;
    public int currentBuildingPopulation;
    public int maxBuildingPopulation;

    public enum HabitatBuildingType { SmallSettlement, Base, City };

    public HabitatBuilding(HabitatBuildingType buildingType, int currentBuildingPopulation)
    {
        this.buildingType = buildingType;
        this.currentBuildingPopulation = currentBuildingPopulation;

        // assigning the max population depending on what type of habitat building this is
        switch (buildingType)
        {
            case HabitatBuildingType.SmallSettlement:
                maxBuildingPopulation = 10;
                break;
            case HabitatBuildingType.Base:
                maxBuildingPopulation = 100;
                break;
            case HabitatBuildingType.City:
                maxBuildingPopulation = 100;
                break;
        }
    }
}
