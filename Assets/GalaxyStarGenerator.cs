using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GalaxyStarGenerator : MonoBehaviour
{
    int starAmountToGenerate = 100;

    [SerializeField] public int numberOfPoints;
    [SerializeField] public float turnFraction;
    [SerializeField] public float distanceFactor;
    [SerializeField] public float locationNoise;

    public bool increment = false;
    public bool calculate = false;
    public bool isGenerating = false;
    public float incrementValue;

    [SerializeField] public float secondaryNumberOfPoints;
    [SerializeField] public float secondaryTurnFraction;
    [SerializeField] public float secondaryDistanceFactor;
    [SerializeField] public float secondaryLocationNoise;

    List<Vector2> starLocations = new List<Vector2>();

    public GameObject classMStarPrefab;
    public GameObject classKStarPrefab;
    public GameObject classGStarPrefab;
    public GameObject classFStarPrefab;
    public GameObject classAStarPrefab;
    public GameObject classBStarPrefab;
    public GameObject classOStarPrefab;

    public GameObject testCubePrefabBlue;
    public GameObject testCubePrefabRed;
    public GameObject testCubePrefabGreen;

    float[] linspace;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < numberOfPoints; i++)
        {
            float distance = i / (numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * turnFraction * 1;

            float x = distance* Mathf.Cos(angle) * distanceFactor;
            float y = distance* Mathf.Sin(angle) * distanceFactor;

            starLocations.Add(new Vector2(x, y));
        }

        for (int i = 0; i < starAmountToGenerate; i++)
        {
            int starClassRNG = Random.Range(0, 100);
            if (starClassRNG < 40) // M class 40%
            {
                Instantiate(classMStarPrefab, new Vector3(starLocations[i].x, 0 , starLocations[i].y), Quaternion.identity);
            }
            else if (starClassRNG >= 40 && starClassRNG < 60) // K class 20%
            {
                Instantiate(classKStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);
            }
            else if (starClassRNG >= 60 && starClassRNG < 75) // G class 15%
            {
                Instantiate(classGStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);
            }
            else if (starClassRNG <= 75 && starClassRNG < 87) // F class 12%
            {
                Instantiate(classFStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);
            }
            else if (starClassRNG >= 87 && starClassRNG < 97) // A class 10%
            {
                Instantiate(classAStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);

            }   
            else if (starClassRNG >= 97 && starClassRNG < 99) // B class 2%
            {
                Instantiate(classBStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);
            }
            else if (starClassRNG == 99) // O class 1%
            {
                Instantiate(classOStarPrefab, new Vector3(starLocations[i].x, 0, starLocations[i].y), Quaternion.identity);
            }
            StarClass starClass = (StarClass)Random.Range(0, 6);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (calculate || isGenerating)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("testCube");
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
                Debug.Log("destroyed testCube");
            }

            for (int i = 0; i < numberOfPoints; i++)
            {
                float distance = i / (numberOfPoints - 1f);
                float angle = 2 * Mathf.PI * turnFraction * i;

                float x = distance * Mathf.Cos(angle) * distanceFactor;
                float z = distance * Mathf.Sin(angle) * distanceFactor;

                float distanceToCenter = Vector3.Distance(new Vector3(x, 0, z), Vector3.zero);
                float t = Mathf.InverseLerp(distanceFactor, 0, distanceToCenter);
                float output = Mathf.Lerp(0, locationNoise, t);

                float noiseX = Random.Range(-output, output);
                float noiseZ = Random.Range(-output, output);

                x += noiseX;
                z += noiseZ;

                GameObject star;
                if (i % 2 == 0)
                {
                    star =  Instantiate(testCubePrefabRed, new Vector3(x, 0, z), Quaternion.identity);
                }
                else
                {
                    star = Instantiate(testCubePrefabBlue, new Vector3(x, 0, z), Quaternion.identity);
                }

                /*if((Vector3.Distance(star.transform.position, Vector3.zero) > 80000))
                {
                    Destroy(star);
                }*/

            }

            for (int i = 0; i < secondaryNumberOfPoints; i++)
            {
                float distance = i / (secondaryNumberOfPoints - 1f);
                float angle = 2 * Mathf.PI * secondaryTurnFraction * i;

                float x = distance * Mathf.Cos(angle) * secondaryDistanceFactor;
                float z = distance * Mathf.Sin(angle) * secondaryDistanceFactor;

                /*float distanceToCenter = Vector3.Distance(new Vector3(x, 0, z), Vector3.zero);
                float t = Mathf.InverseLerp(secondaryDistanceFactor, 0, distanceToCenter);
                float output = Mathf.Lerp(0, secondaryLocationNoise, t);*/

                float noiseX = Random.Range(-secondaryLocationNoise, secondaryLocationNoise);
                float noiseZ = Random.Range(-secondaryLocationNoise, secondaryLocationNoise);

                x += noiseX;
                z += noiseZ;

                GameObject star;
                star = Instantiate(testCubePrefabGreen, new Vector3(x, 0, z), Quaternion.identity);

                /*if((Vector3.Distance(star.transform.position, Vector3.zero) > 80000))
                {
                    Destroy(star);
                }*/

            }

            calculate = false;
        }
        if (increment) turnFraction += incrementValue;
    }
}
