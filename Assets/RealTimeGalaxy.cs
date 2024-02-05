using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeGalaxy : MonoBehaviour
{
    public GalaxyFactory galaxyFactory;
    [Header("One Time Buttons")]
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

    // Update is called once per frame
    void Update()
    {
        if (generate)
        {
            DateTime time = DateTime.Now;

            clearChildren();
            galaxyFactory.generateCubesGalaxy(galaxyFactory.getDiskSettings());

            generate = false;

            TimeSpan time2 = DateTime.Now - time;
            Debug.Log("generate time: " + time2);
        }
        else if (clear)
        {
            DateTime time = DateTime.Now;

            clearChildren();
            clear = false;

            TimeSpan time2 = DateTime.Now - time;
            Debug.Log("clear time: " + time2);
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
