using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    public int index;
    public bool isHomeworld;

    public void makeHomeWorld()
    {
        isHomeworld = true;
    }
}
