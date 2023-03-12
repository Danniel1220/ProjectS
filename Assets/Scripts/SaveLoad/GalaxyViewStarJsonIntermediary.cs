using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GalaxyViewStarJsonIntermediary
{
    public float[] transform;
    public int starClass;

    public GalaxyViewStarJsonIntermediary(float[] transform, int starClass)
    {
        this.transform = transform;
        this.starClass = starClass;
    }
}
