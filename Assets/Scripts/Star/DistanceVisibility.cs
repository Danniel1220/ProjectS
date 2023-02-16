using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVisibility : MonoBehaviour
{
    public float visibilityRange;

    private ParticleSystem particleSystem; 

    float distance;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        // if the camera is too close to the star
        if (distance < visibilityRange)
        {
            //particleSystem.Stop();
            //particleSystem.Clear(true); // true means we also clear the children of the particle fx
        }
        else
        {
            //particleSystem.Play();
        }
    }
}
