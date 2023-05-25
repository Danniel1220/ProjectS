using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlanetGenerationTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlanetFactory.generatePlanet(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
