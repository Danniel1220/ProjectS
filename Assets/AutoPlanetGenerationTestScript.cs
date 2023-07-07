using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlanetGenerationTestScript : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<PlanetFactory>().generatePlanet(this.transform, 0f);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
    } 
}
