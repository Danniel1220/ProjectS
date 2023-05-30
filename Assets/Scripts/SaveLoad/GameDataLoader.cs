using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    // caching the star prefab refferences
    private GameObject classMStarPrefab;
    private GameObject classKStarPrefab;
    private GameObject classGStarPrefab;
    private GameObject classFStarPrefab;
    private GameObject classAStarPrefab;
    private GameObject classBStarPrefab;
    private GameObject classOStarPrefab;

    void Start()
    {
        classMStarPrefab = StarPrefabs.classMStar;
        classKStarPrefab = StarPrefabs.classKStar;
        classGStarPrefab = StarPrefabs.classGStar;
        classFStarPrefab = StarPrefabs.classFStar;
        classAStarPrefab = StarPrefabs.classAStar;
        classBStarPrefab = StarPrefabs.classBStar;
        classOStarPrefab = StarPrefabs.classOStar;

    }

    public void loadGameData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SavedGameData.json");
        GameDataSerializableWrapper gameData = JsonConvert.DeserializeObject<GameDataSerializableWrapper>(json);


    }
}
