using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int colonyPacks;

    void Start()
    {
        colonyPacks = 10;        
    }

    public void useColonyPack()
    {
        colonyPacks--;
    }
}
