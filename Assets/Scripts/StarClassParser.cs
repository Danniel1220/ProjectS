using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StarClassParser
{
    public static StarClass parseGameObjectTagToStarClass(string tag)
    {
        switch (tag)
        {
            case "Class M Star":
                return StarClass.M;
            case "Class K Star":
                return StarClass.K;
            case "Class G Star":
                return StarClass.G;
            case "Class F Star":
                return StarClass.F;
            case "Class A Star":
                return StarClass.A;
            case "Class B Star":
                return StarClass.B;
            case "Class O Star":
                return StarClass.O;
            default:
                Debug.LogError("StarClassParser found received faulty tag: " + tag + "\nDefaulting to StarClass.M");
                return StarClass.M;
        }
    }
}
