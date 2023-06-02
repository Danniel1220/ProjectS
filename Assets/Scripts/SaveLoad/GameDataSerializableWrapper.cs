using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataSerializableWrapper
{
    public float starshipTransformX;
    public float starshipTransformY;
    public float starshipTransformZ;
    public int starshipTargetIndex;
    public List<StarSystemSerializableDataWrapper> starSystems;

    public GameDataSerializableWrapper(float starshipTransformX, float starshipTransformY, float starshipTransformZ, int starshipTargetIndex, List<StarSystemSerializableDataWrapper> starSystems)
    {
        this.starshipTransformX = starshipTransformX;
        this.starshipTransformY = starshipTransformY;
        this.starshipTransformZ = starshipTransformZ;
        this.starshipTargetIndex = starshipTargetIndex;
        this.starSystems = starSystems;
    }
}
