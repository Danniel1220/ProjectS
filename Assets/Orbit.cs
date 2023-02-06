using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitTargetTransform;
    private Transform planetTransform;
    public float orbitSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        planetTransform = GetComponent<Transform>();
        orbitSpeed = Random.Range(10, 40);
        rotationSpeed = Random.Range(2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        planetTransform.RotateAround(planetTransform.position, planetTransform.up, rotationSpeed * Time.deltaTime);
        planetTransform.RotateAround(orbitTargetTransform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
