using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeParameters : MonoBehaviour
{
    public static int maxHabitatBuildingsPerSector;
    public static int maxStorageBuildingsPerSector;
    public static int maxEnergyBuildingsPerSector;
    public static int maxMiningBuildingsPerSector;
    public static int maxProductionBuildingsPerSector;
    public static int maxScienceBuildingsPerSector;


    void Start()
    {
        maxHabitatBuildingsPerSector = 2;
        maxStorageBuildingsPerSector = 2;
        maxEnergyBuildingsPerSector = 1;
        maxMiningBuildingsPerSector = 1;
        maxProductionBuildingsPerSector= 1;
        maxScienceBuildingsPerSector = 1;
    }
}
