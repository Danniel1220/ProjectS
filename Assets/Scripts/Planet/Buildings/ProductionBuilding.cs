using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding
{
    public ProductionBuildingType buildingType;
    public int craftingSpeed;
    public int populationUsage;
    public int energyUsage;

    public enum ProductionBuildingType { AssemblyMachine, ProductionFacility, Factory };

    public ProductionBuilding(ProductionBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the storage slots depending on what type of habitat building this is
        switch (buildingType)
        {
            case ProductionBuildingType.AssemblyMachine:
                craftingSpeed = 1;
                populationUsage = 1;
                energyUsage = 2;
                break;
            case ProductionBuildingType.ProductionFacility:
                craftingSpeed = 5;
                populationUsage = 2;
                energyUsage = 5;
                break;
            case ProductionBuildingType.Factory:
                craftingSpeed = 10;
                populationUsage = 5;
                energyUsage = 10;
                break;
        }
    }
}
