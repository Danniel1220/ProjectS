using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StarSystemSerializableDataWrapper
{
    public float transformX;
    public float transformY;
    public float transformZ;
    public string starClass;
    public string name;
    public List<PlanetSerializableDataWrapper> planets = new List<PlanetSerializableDataWrapper>();

    public StarSystemSerializableDataWrapper(float transformX, float transformY, float transformZ, string starClass, string name, List<PlanetSerializableDataWrapper> planets)
    {
        this.transformX = transformX;
        this.transformY = transformY;
        this.transformZ = transformZ;
        this.starClass = starClass;
        this.name = name;
        this.planets = planets;
    }
}
