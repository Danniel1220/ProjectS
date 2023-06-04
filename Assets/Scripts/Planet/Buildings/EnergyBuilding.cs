using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuilding
{
    public EnergyBuildingType buildingType;
    public int outputEnergy;

    public enum EnergyBuildingType { SolarPanel, SolarFarm };

    public EnergyBuilding(EnergyBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the output energy depending on what type of energy building this is
        switch (buildingType)
        {
            case EnergyBuildingType.SolarPanel:
                outputEnergy = 2;
                break;
            case EnergyBuildingType.SolarFarm:
                outputEnergy = 200;
                break;
        }
    }
}
