using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StarSystemSerializableDataWrapper
{
    Transform starSystemTransform;
    StarClass starClass;
    List<PlanetSerializableDataWrapper> planets = new List<PlanetSerializableDataWrapper>();
}
