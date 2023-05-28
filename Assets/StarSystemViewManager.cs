using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemViewManager : MonoBehaviour
{
    private StarFactory starFactory;
    private GalaxyViewShipPosition galaxyViewShipPosition;
    void Start()
    {
        starFactory = GameManagers.starFactory;
        galaxyViewShipPosition = GameObject.Find("Starship").GetComponent<GalaxyViewShipPosition>();
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
