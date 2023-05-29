using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFunctionsWrapper : MonoBehaviour
{
    public void saveGameData()
    {
        Debug.Log("Saving game data...");
        GameDataSaver.saveGameData();
    }

    public void loadGameData()
    {
        Debug.Log("Loading game data...");
        GameDataLoader.loadGameData();
    }
}
