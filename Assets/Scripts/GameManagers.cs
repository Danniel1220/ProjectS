using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManagers
{
    public static GalaxyChunkSystem galaxyChunkSystem = GameObject.Find("GameManager").GetComponent<GalaxyChunkSystem>();

    public static StarFactory starFactory = GameObject.Find("GameManager").GetComponent<StarFactory>();
    public static PlanetFactory planetFactory = GameObject.Find("GameManager").GetComponent<PlanetFactory>();

    public static GameDataSaver gameDataSaver = GameObject.Find("GameManager").GetComponent<GameDataSaver>();
    public static GameDataLoader gameDataLoader = GameObject.Find("GameManager").GetComponent<GameDataLoader>();
}
