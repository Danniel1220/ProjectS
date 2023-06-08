using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuilding
{
    public EnergyBuildingType buildingType;
    public int energyOutput;

    public enum EnergyBuildingType { SolarPanel, SolarFarm };

    public EnergyBuilding(EnergyBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the output energy depending on what type of energy building this is
        switch (buildingType)
        {
            case EnergyBuildingType.SolarPanel:
                energyOutput = 2;
                break;
            case EnergyBuildingType.SolarFarm:
                energyOutput = 200;
                break;
        }
    }
}
