using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeGalaxy : MonoBehaviour
{
    public GalaxyFactory galaxyFactory;
    [Header("Generation")]
    public bool generate = false;
    public bool clear = false;

    [Header("Real Time")]
    public bool running = false;
    public bool runningPrimary = false;
    public bool runningSecondary = false;

    [Header("Turn Fraction Incrementing")]
    public bool incrementPrimaryTurnFraction = false;
    public bool incrementSecondaryTurnFraction = false;
    public float turnSpeed = 0.0001f;


    [Header("Increase/Decrease Galaxy Size")]
    public bool applyPercentage;
    public float percentage;

    private bool frameSkipAux;

    void Start()
    {
        frameSkipAux = false;
    }

    void Update()
    {
        if (generate)
        {
            

            if (frameSkipAux == false)
            {
                DateTime time = DateTime.Now;
                clearChildren();
                frameSkipAux = true;
                TimeSpan time2 = DateTime.Now - time;
                Debug.Log("clear time: " + time2);
            }
            else
            {
                DateTime time = DateTime.Now;
                galaxyFactory.generateCubesGalaxy(galaxyFactory.getDiskSettings());
                generate = false;
                frameSkipAux = false;
                TimeSpan time2 = DateTime.Now - time;
                Debug.Log("generate time: " + time2);
            }
        }

        if (clear)
        {
            DateTime time = DateTime.Now;

            clearChildren();
            clear = false;

            TimeSpan time2 = DateTime.Now - time;
            Debug.Log("clear time: " + time2);
        }

        if (applyPercentage)
        {
            galaxyFactory.changeGalaxySizeParams(percentage);
            applyPercentage = false;
        }
    }

    public void clearChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
