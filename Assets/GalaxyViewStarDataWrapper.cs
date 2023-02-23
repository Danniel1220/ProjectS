using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GalaxyViewStarDataWrapper
{
    public List<GalaxyViewStarSerializableData> starData;

    public GalaxyViewStarDataWrapper(List<GalaxyViewStarSerializableData> starData)
    {
        this.starData = starData;
    }
}
