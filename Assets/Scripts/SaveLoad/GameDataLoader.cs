using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    private StarFactory starFactory;
    private PlanetFactory planetFactory;

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
        starFactory = GameManagers.starFactory;
        planetFactory = GameManagers.planetFactory;

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

        foreach (StarSystemSerializableDataWrapper starSystem in gameData.starSystems)
        {
            Vector3 starSystemLocation = new Vector3(starSystem.transformX, starSystem.transformY, starSystem.transformZ);

            //starFactory.createStarSystem(starSystemLocation, StarClassParser.stringToStarClass(starSystem.starClass));
        }
    }
}
