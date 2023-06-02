using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemViewManager : MonoBehaviour
{
    private StarFactory starFactory;
    private StarshipPosition starshipPosition;
    private GameObject starSystemCurrentlyInView;

    void Start()
    {
        starFactory = GameManagers.starFactory;
        starshipPosition = GameObject.Find("Starship").GetComponent<StarshipPosition>();
    }

    public void enterStarSystemView()
    {
        starFactory.disableAllStarSystemsButOne(starshipPosition.getTargetObject());
        starSystemCurrentlyInView = starshipPosition.getTargetObject();
    }

    public void exitStarSystemView()
    {
        starFactory.enableAllStarsSystems();
    }
}
