using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers: MonoBehaviour
{
    public static ChunkSystem galaxyChunkSystem;

    public static GameObject starSystemsContainer;

    public static StarFactory starFactory;
    public static PlanetFactory planetFactory;

    public static GameDataSaver gameDataSaver;
    public static GameDataLoader gameDataLoader;

    void Awake()
    {
        galaxyChunkSystem = GameObject.Find("GameManager").GetComponent<ChunkSystem>();

        starSystemsContainer = GameObject.Find("Star Systems Container");

        starFactory = GameObject.Find("GameManager").GetComponent<StarFactory>();
        planetFactory = GameObject.Find("GameManager").GetComponent<PlanetFactory>();

        gameDataSaver = GameObject.Find("GameManager").GetComponent<GameDataSaver>();
        gameDataLoader = GameObject.Find("GameManager").GetComponent<GameDataLoader>();
    }
}
