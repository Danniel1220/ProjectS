using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilter
{
    float evaluateNoise(Vector3 point);
}
