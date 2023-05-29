using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StarSystemSerializableDataWrapper
{
    public Transform transform;
    public StarClass starClass;
    public string name;
    public List<PlanetSerializableDataWrapper> planets = new List<PlanetSerializableDataWrapper>();

    public StarSystemSerializableDataWrapper(Transform transform, StarClass starClass, string name, List<PlanetSerializableDataWrapper> planets)
    {
        this.transform = transform;
        this.starClass = starClass;
        this.name = name;
        this.planets = planets;
    }
}
