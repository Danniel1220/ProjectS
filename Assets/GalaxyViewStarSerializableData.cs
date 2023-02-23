using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GalaxyViewStarSerializableData
{
    public Vector3 position;
    public string starClass;

    public GalaxyViewStarSerializableData(Vector3 position, string starClass)
    {
        this.position = position;
        this.starClass = starClass;
    }
}
