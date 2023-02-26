using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GalaxyViewStarSerializableData
{
    public float posX;
    public float posY;
    public float posZ;
    public string starClass;

    public GalaxyViewStarSerializableData(float posX, float posY, float posZ, string starClass)
    {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.starClass = starClass;
    }
}
