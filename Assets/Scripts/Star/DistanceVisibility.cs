using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVisibility : MonoBehaviour
{
    public float visibilityRange;

    //getting camera object to calculate distance from particle to camera to disable the particle if we are too close
    private new ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        // if the camera is too close to the star
        if (distance < visibilityRange)
        {
            particleSystem.Stop();
            particleSystem.Clear(true); // true means we also clear the children of the particle fx
        }
        else
        {
            particleSystem.Play();
        }
    }
}
