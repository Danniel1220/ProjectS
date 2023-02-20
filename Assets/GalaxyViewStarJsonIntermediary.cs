using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewStarJsonIntermediary
{
    public float transformX;
    public float transformY;
    public float transformZ;

    public StarClass starClass;

    public GalaxyViewStarJsonIntermediary(float transformX, float transformY, float transformZ, StarClass starClass)
    {
        this.transformX = transformX;
        this.transformY = transformY;
        this.transformZ = transformZ;
        this.starClass = starClass;
    }
}
