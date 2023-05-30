using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private Transform orbitTargetTransform;
    private Transform planetTransform;
    public float orbitSpeed;
    public float rotationSpeed;
    // this doesnt serve any purpose here per se
    // but i have to store this information somewhere for saving/loading it and here makes the most sense
    public float orbitDistance;

    void Start()
    {
        planetTransform = GetComponent<Transform>();
    }

    void Update()
    {
        planetTransform.RotateAround(planetTransform.position, planetTransform.up, rotationSpeed * Time.deltaTime);
        planetTransform.RotateAround(orbitTargetTransform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

    public void setOrbitParameters(Transform orbitTargetTransform, float orbitSpeed, float rotationSpeed, float orbitalDistance)
    {
        this.orbitTargetTransform = orbitTargetTransform;
        this.orbitSpeed = orbitSpeed;
        this.rotationSpeed = rotationSpeed;

        this.orbitDistance = orbitalDistance;

        this.transform.localPosition = new Vector3(orbitDistance, 0f, 0f);
    }
}
