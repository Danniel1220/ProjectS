using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers
{
    public static GalaxyChunkSystem galaxyChunkSystem = GameObject.Find("GameManager").GetComponent<GalaxyChunkSystem>();
    public static StarFactory starFactory = GameObject.Find("GameManager").GetComponent<StarFactory>();
}
