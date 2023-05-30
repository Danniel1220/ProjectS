using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StarClassParser
{
    public static StarClass gameObjectTagToStarClass(string tag)
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
                Debug.LogError("StarClassParser.gameObjectTagToStarClass defaulted to class M given input argument: '" + tag + "'");
                return StarClass.M;
        }
    }

    public static StarClass stringToStarClass(string starClassString)
    {
        switch (starClassString)
        {
            case "M":
                return StarClass.M;
            case "K":
                return StarClass.K;
            case "G":
                return StarClass.G;
            case "F":
                return StarClass.F;
            case "A":
                return StarClass.A;
            case "B":
                return StarClass.B;
            case "O":
                return StarClass.O;
            default:
                Debug.LogError("StarClassParser.stringToStarClass defaulted to class M given input argument: '" + starClassString + "'");
                return StarClass.M;
        }
    }

    public static string starClassToString(StarClass starClass)
    {
        switch (starClass)
        {
            case StarClass.M:
                return "M";
            case StarClass.K:
                return "K";
            case StarClass.G:
                return "G";
            case StarClass.F:
                return "F";
            case StarClass.A:
                return "A";
            case StarClass.B:
                return "B";
            case StarClass.O:
                return "O";
            default:
                Debug.LogError("StarClassParser.starClassToString defaulted to class M given input argument: '" + starClass + "'");
                return "M";
        }
    }
}
