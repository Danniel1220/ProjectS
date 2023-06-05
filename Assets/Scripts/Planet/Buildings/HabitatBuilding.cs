using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatBuilding
{
    public HabitatBuildingType buildingType;
    public int population;

    public enum HabitatBuildingType { SmallSettlement, Base, City };

    public HabitatBuilding(HabitatBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the max population depending on what type of habitat building this is
        switch (buildingType)
        {
            case HabitatBuildingType.SmallSettlement:
                population = 10;
                break;
            case HabitatBuildingType.Base:
                population = 100;
                break;
            case HabitatBuildingType.City:
                population = 1000;
                break;
        }
    }
}
