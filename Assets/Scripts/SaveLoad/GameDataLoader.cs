using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDataLoader
{
    // caching the star prefab refferences
    private static GameObject classMStarPrefab = StarPrefabs.classMStar;
    private static GameObject classKStarPrefab = StarPrefabs.classKStar;
    private static GameObject classGStarPrefab = StarPrefabs.classGStar;
    private static GameObject classFStarPrefab = StarPrefabs.classFStar;
    private static GameObject classAStarPrefab = StarPrefabs.classAStar;
    private static GameObject classBStarPrefab = StarPrefabs.classBStar;
    private static GameObject classOStarPrefab = StarPrefabs.classOStar;

    public static void loadGameData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SavedGameData.json");
        GameDataSerializableWrapper gameData = JsonConvert.DeserializeObject<GameDataSerializableWrapper>(json);
    }
}
