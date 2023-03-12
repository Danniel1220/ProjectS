using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemViewManager : MonoBehaviour
{
    private StarHelper starHelper;
    private GalaxyViewShipPosition galaxyViewShipPosition;
    void Start()
    {
        starHelper = GameObject.Find("StarManager").GetComponent<StarHelper>();
        galaxyViewShipPosition = GameObject.Find("Starship").GetComponent<GalaxyViewShipPosition>();
    }

    public void enterStarSystemView()
    {
        starHelper.disableAllStarSystemsButOne(galaxyViewShipPosition.getTargetPosition());
    }

    public void exitStarSystemView()
    {
        starHelper.enableAllStarsSystems();
    }
}
