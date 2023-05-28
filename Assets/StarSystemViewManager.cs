using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemViewManager : MonoBehaviour
{
    private StarFactory starHelper;
    private GalaxyViewShipPosition galaxyViewShipPosition;
    void Start()
    {
        starHelper = GameObject.Find("StarManager").GetComponent<StarFactory>();
        galaxyViewShipPosition = GameObject.Find("Starship").GetComponent<GalaxyViewShipPosition>();
    }

    public void enterStarSystemView()
    {
        starHelper.disableAllStarSystemsButOne(galaxyViewShipPosition.getTargetStarSystem());
    }

    public void exitStarSystemView()
    {
        starHelper.enableAllStarsSystems();
    }
}
