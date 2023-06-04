using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HabitatBuilding;

public class StorageBuilding
{
    public StorageBuildingType buildingType;
    public int maxStorageSlots;

    public enum StorageBuildingType { Container, Warehouse };

    public StorageBuilding(StorageBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the storage slots depending on what type of storage building this is
        switch (buildingType)
        {
            case StorageBuildingType.Container:
                maxStorageSlots = 10;
                break;
            case StorageBuildingType.Warehouse:
                maxStorageSlots = 40; 
                break;
        }
    }
}
