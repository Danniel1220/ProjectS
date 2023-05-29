using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataSerializableWrapper
{
    public List<StarSystemSerializableDataWrapper> starSystems;

    public GameDataSerializableWrapper(List<StarSystemSerializableDataWrapper> starSystems)
    {
        this.starSystems = starSystems;
    }
}
