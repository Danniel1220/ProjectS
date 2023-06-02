using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers: MonoBehaviour
{
    public static ChunkSystem chunkSystem;

    public static GameObject starSystemsContainer;

    public static StarFactory starFactory;
    public static PlanetFactory planetFactory;

    public static GameDataSaver gameDataSaver;
    public static GameDataLoader gameDataLoader;

    public static StarshipPosition starshipPosition;

    public static HomeworldDesignator homeworldDesignator;

    public static NameGenerator nameGenerator;

    public static Inventory starshipInventory;

    void Awake()
    {
        chunkSystem = GameObject.Find("GameManager").GetComponent<ChunkSystem>();

        starSystemsContainer = GameObject.Find("Star Systems Container");

        starFactory = GameObject.Find("GameManager").GetComponent<StarFactory>();
        planetFactory = GameObject.Find("GameManager").GetComponent<PlanetFactory>();

        gameDataSaver = GameObject.Find("GameManager").GetComponent<GameDataSaver>();
        gameDataLoader = GameObject.Find("GameManager").GetComponent<GameDataLoader>();

        starshipPosition = GameObject.Find("Starship").GetComponent<StarshipPosition>();

        homeworldDesignator = GameObject.Find("GameManager").GetComponent<HomeworldDesignator>();

        nameGenerator = GameObject.Find("GameManager").GetComponent<NameGenerator>();

        starshipInventory = GameObject.Find("Starship").GetComponent<Inventory>();
    }
}
