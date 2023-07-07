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
            galaxyFactory.setDiskSettings();
            galaxyFactory.createTestCubesOnDisk(galaxyFactory.primaryDiskSettings, true);
            generate = false;
        }
        if (clear)
        {
            clearChildren();
            clear = false;
        }
        if (running)
        {
            clearChildren();
            galaxyFactory.setDiskSettings();

            if (runningPrimary)
            {
                galaxyFactory.createTestCubesOnDisk(galaxyFactory.primaryDiskSettings, true);
            }
            if (runningSecondary)
            {
                galaxyFactory.createTestCubesOnDiskGreen(galaxyFactory.secondaryDiskSettings, true);
            }

            if (incrementPrimaryTurnFraction)
            {
                galaxyFactory.primaryTurnFraction += turnSpeed * Time.deltaTime;
            }
            if (incrementSecondaryTurnFraction)
            {
                galaxyFactory.secondaryTurnFraction += turnSpeed * Time.deltaTime;
            }
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
