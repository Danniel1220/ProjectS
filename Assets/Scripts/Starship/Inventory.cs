using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int colonyPacks;
    public int habitatSectorInfrastrucutes;
    public int storageSectorInfrastructures;
    public int energySectorInfrastructures;
    public int miningSectorInfrastructures;
    public int productionSectorInfrastructures;
    public int scienceSectorInfrastructures;

    void Start()
    {
        colonyPacks = 10;      
        habitatSectorInfrastrucutes = 10;
        storageSectorInfrastructures = 10;
        energySectorInfrastructures = 10;
        miningSectorInfrastructures = 10;
        productionSectorInfrastructures = 10;
        scienceSectorInfrastructures = 10;
    }

    public void useColonyPack()
    {
        colonyPacks--;
    }

    public void useHabitatSectorInfrastructure()
    {
        habitatSectorInfrastrucutes--;
    }

    public void useStorageSectorInfrastructure()
    {
        storageSectorInfrastructures--;
    }

    public void useEnergySectorInfrastructure()
    {
        energySectorInfrastructures--;
    }

    public void useMiningSectorInfrastructure()
    {
        miningSectorInfrastructures--;
    }

    public void useProductionSectorInfrastructure()
    {
        productionSectorInfrastructures--;
    }

    public void useScienceSectorInfrastructure()
    {
        scienceSectorInfrastructures--;
    }
}
