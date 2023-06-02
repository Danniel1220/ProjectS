using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string name;
    public string symbol;
    public float percentageByPlanetWeight;

    public Resource(string name, string symbol, float percentageByPlanetWeight)
    {
        this.name = name;
        this.symbol = symbol;
        this.percentageByPlanetWeight = percentageByPlanetWeight;
    }
}
