using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatBuilding
{
    public HabitatBuildingType buildingType;
    public int population;
    public int energyUsage;

    public enum HabitatBuildingType { SmallSettlement, Base, City };

    public HabitatBuilding(HabitatBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the max population depending on what type of habitat building this is
        switch (buildingType)
        {
            case HabitatBuildingType.SmallSettlement:
                population = 10;
                energyUsage = 1;
                break;
            case HabitatBuildingType.Base:
                population = 100;
                energyUsage = 5;
                break;
            case HabitatBuildingType.City:
                population = 1000;
                energyUsage = 10;
                break;
        }
    }
}
