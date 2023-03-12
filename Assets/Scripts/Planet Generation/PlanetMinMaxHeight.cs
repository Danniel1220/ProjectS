using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMinMaxHeight
{
    public float min { get; private set; }
    public float max { get; private set; }

    public PlanetMinMaxHeight()
    {
        this.min = float.MaxValue;
        this.max = float.MinValue;
    }

    public void addValue(float v)
    {
        if (v > max) max = v;
        if (v < min) min = v;
    }
}
