using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding
{
    public MiningBuildingType buildingType;
    public int craftingSpeed;

    public enum MiningBuildingType { AssemblyMachine, ProductionFacility, Factory };

    public ProductionBuilding(MiningBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the storage slots depending on what type of habitat building this is
        switch (buildingType)
        {
            case MiningBuildingType.AssemblyMachine:
                craftingSpeed = 1;
                break;
            case MiningBuildingType.ProductionFacility:
                craftingSpeed = 5;
                break;
            case MiningBuildingType.Factory:
                craftingSpeed = 10;
                break;
        }
    }
}
