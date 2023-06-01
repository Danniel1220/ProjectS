using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemViewManager : MonoBehaviour
{
    private StarFactory starFactory;
    private StarshipPosition galaxyViewShipPosition;
    void Start()
    {
        starFactory = GameManagers.starFactory;
        galaxyViewShipPosition = GameObject.Find("Starship").GetComponent<StarshipPosition>();
    }

    public void enterStarSystemView()
    {
        starFactory.disableAllStarSystemsButOne(galaxyViewShipPosition.getTargetStarSystem());
    }

    public void exitStarSystemView()
    {
        starFactory.enableAllStarsSystems();
    }
}
