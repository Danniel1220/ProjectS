using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding
{
    public ProductionBuildingType buildingType;
    public int craftingSpeed;

    public enum ProductionBuildingType { AssemblyMachine, ProductionFacility, Factory };

    public ProductionBuilding(ProductionBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the storage slots depending on what type of habitat building this is
        switch (buildingType)
        {
            case ProductionBuildingType.AssemblyMachine:
                craftingSpeed = 1;
                break;
            case ProductionBuildingType.ProductionFacility:
                craftingSpeed = 5;
                break;
            case ProductionBuildingType.Factory:
                craftingSpeed = 10;
                break;
        }
    }
}
