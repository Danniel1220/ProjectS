using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacteristics : MonoBehaviour
{
    private Transform planetTransform;
    public float planetDiameter;

    // Start is called before the first frame update
    void Start()
    {
        planetTransform = GetComponent<Transform>();
        planetDiameter = Random.Range(20, 100);

        planetTransform.localScale = new Vector3(planetDiameter, planetDiameter, planetDiameter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
