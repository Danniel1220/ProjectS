using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static PlanetInfo planetInfoPanel;
    public static PlanetMenu planetMenuPanel;
    public static NewBuildingMenu newBuildingPanel;
    public static SectorsStats sectorStats;
    public static StoragePanel storagePanel;

    void Awake()
    {
        planetInfoPanel = GameObject.Find("Canvas").transform.Find("PlanetInfo").GetComponent<PlanetInfo>();
        planetMenuPanel = GameObject.Find("Canvas").transform.Find("PlanetMenu").GetComponent<PlanetMenu>();
        newBuildingPanel = GameObject.Find("Canvas").transform.Find("NewBuildingMenu").GetComponent<NewBuildingMenu>();
        sectorStats = GameObject.Find("Canvas").transform.Find("PlanetMenu").Find("SectorsStats").GetComponent<SectorsStats>();
        storagePanel = GameObject.Find("Canvas").transform.Find("PlanetMenu").Find("Storage").GetComponent<StoragePanel>();
    }
}
