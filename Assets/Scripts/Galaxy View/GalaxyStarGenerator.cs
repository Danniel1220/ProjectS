using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GalaxyStarGenerator : MonoBehaviour
{
    [SerializeField] private int primaryNumberOfPoints;
    [SerializeField] private float primaryTurnFraction;
    [SerializeField] private float primaryDistanceFactor;
    [SerializeField] private float primaryLocationNoiseXZ;
    [SerializeField] private float primaryLocationNoiseY;

    [SerializeField] private int secondaryNumberOfPoints;
    [SerializeField] private float secondaryTurnFraction;
    [SerializeField] private float secondaryDistanceFactor;
    [SerializeField] private float secondaryLocationNoiseXZ;
    [SerializeField] private float secondaryLocationNoiseY;

    [SerializeField] private float galaxyMaxDiameter;

    [SerializeField] private float minDistanceBetweenPoints;

    [SerializeField] private bool increment = false;
    [SerializeField] private bool calculate = false;
    [SerializeField] private bool isGenerating = false;
    [SerializeField] private float incrementValue;

    [SerializeField] private GameObject classMStarPrefab;
    [SerializeField] private GameObject classKStarPrefab;
    [SerializeField] private GameObject classGStarPrefab;
    [SerializeField] private GameObject classFStarPrefab;
    [SerializeField] private GameObject classAStarPrefab;
    [SerializeField] private GameObject classBStarPrefab;
    [SerializeField] private GameObject classOStarPrefab;

    [SerializeField] private GameObject testCubePrefabBlue;
    [SerializeField] private GameObject testCubePrefabRed;
    [SerializeField] private GameObject testCubePrefabGreen;

    List<GameObject> pointList = new List<GameObject>();

    private GalaxyChunkSystem galaxyChunkSystem;

    // 3 dimensional array that will store all the stars in a chunk based system
    // the first 2 dimensions are used for the chunk grid
    // the last dimension is used to store all the points in one specific chunk
    List<List<List<GameObject>>> chunkGrid = new List<List<List<GameObject>>>();

    public Text UIText;

    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();

        primaryNumberOfPoints = 30000;
        primaryTurnFraction = 0.750016f;
        primaryDistanceFactor = 100000;
        primaryLocationNoiseXZ = 25000;
        primaryLocationNoiseY = 10000f;

        secondaryNumberOfPoints = 10000;
        secondaryTurnFraction = 1.618034f;
        secondaryDistanceFactor = 130000;
        secondaryLocationNoiseXZ = 10000f;
        secondaryLocationNoiseY = 8000f;

        minDistanceBetweenPoints = 1000f;

        //clearAllTestCubes();

        // primary formation
        createPointsOnDisk(primaryNumberOfPoints, primaryTurnFraction, primaryDistanceFactor, primaryLocationNoiseXZ, primaryLocationNoiseY,
                true, true, true, testCubePrefabBlue, testCubePrefabRed);
        // secondary formation
        createPointsOnDisk(secondaryNumberOfPoints, secondaryTurnFraction, secondaryDistanceFactor, secondaryLocationNoiseXZ, secondaryLocationNoiseY,
            false, true, false, testCubePrefabGreen, testCubePrefabGreen);

        //removeOverlappedPoints(pointList, 1000);
        removeOverlappedPointsV2(minDistanceBetweenPoints);

        /*for (int i = 0; i < starAmountToGenerate; i++)
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

    void Update()
    {
        if (calculate)
        {
            clearAllTestCubes();
            createPointsOnDisk(primaryNumberOfPoints, primaryTurnFraction, primaryDistanceFactor, primaryLocationNoiseXZ, primaryLocationNoiseY, 
                true, true, true, testCubePrefabBlue, testCubePrefabRed);
            createPointsOnDisk(secondaryNumberOfPoints, secondaryTurnFraction, secondaryDistanceFactor, secondaryLocationNoiseXZ, secondaryLocationNoiseY, 
                false, false, true, testCubePrefabGreen, testCubePrefabGreen);

            calculate = false;
        }
    }

    private void clearAllTestCubes()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("testCube");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
    }

    private void createPointsOnDisk(int numberOfPoints, float turnFraction, float distanceFactor, float locationNoiseXZ, float locationNoiseY, 
        bool decreaseXNoiseByDistance, bool decreaseYNoiseByDistance, bool decreaseZNoiseByDistance, GameObject prefabToUse1, GameObject prefabToUse2)
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            // computing points on a circle with a specific turn fraction that forms a galaxy shape
            float distance = i / (numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * turnFraction * i;

            float pointX = distance * Mathf.Cos(angle) * distanceFactor;
            float pointY = 0;
            float pointZ = distance * Mathf.Sin(angle) * distanceFactor;

            float noiseX;
            float noiseY;
            float noiseZ;

            // mapping the distance to center to the inverse of maximum location noise,
            // this way the further away from the center a point is,
            // we can potentially decrease the location noise if required,
            // on any of the 3 axis
            float distanceToCenter = Vector3.Distance(new Vector3(pointX, 0, pointZ), Vector3.zero);
            float mapInput = Mathf.InverseLerp(distanceFactor, 0, distanceToCenter);
            float mapOutputXZ = Mathf.Lerp(0, locationNoiseXZ, mapInput);
            float mapOutputY = Mathf.Lerp(0, locationNoiseY, mapInput);

            if (decreaseXNoiseByDistance) noiseX = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseX = Random.Range(-locationNoiseXZ, locationNoiseXZ);

            if (decreaseYNoiseByDistance) noiseY = Random.Range(-mapOutputY, mapOutputY);
            else noiseY = Random.Range(-locationNoiseY, locationNoiseY);

            if (decreaseZNoiseByDistance) noiseZ = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseZ = Random.Range(-locationNoiseXZ, locationNoiseXZ);

            float pointXAfterNoise = pointX + noiseX;
            float pointYAfterNoise = pointY + noiseY;
            float pointZAfterNoise = pointZ + noiseZ;

            // creating the star
            GameObject point;
            if (i % 2 == 0)
            {
                point = Instantiate(prefabToUse1, new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise), Quaternion.identity);

            }
            else
            {
                point = Instantiate(prefabToUse2, new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise), Quaternion.identity);
            }
            galaxyChunkSystem.addItemToChunk(point);

            /*if ((Vector3.Distance(point.transform.position, Vector3.zero) > galaxyMaxDiameter))
            {
                Destroy(point);
            }
            else
            {
                starList.Add(point);
            }*/

        }
    }

    private void removeOverlappedPoints(List<GameObject> pointList, float minDistance)
    {
        List<GameObject> pointsToRemoveFromList = new List<GameObject>();

        foreach (GameObject pointA in pointList)
        {
            foreach (GameObject pointB in pointList)
            {
                if (pointA != pointB && Vector3.Distance(pointA.transform.position, pointB.transform.position) < minDistance)
                {
                    Destroy(pointA);
                    pointsToRemoveFromList.Add(pointA);
                }
            }
        }

        foreach (GameObject pointToRemove in pointsToRemoveFromList)
        {
            pointList.Remove(pointToRemove);
        }
    }

    private void removeOverlappedPointsV2(float minDistance)
    {
        // for each chunk
        foreach (GalaxyChunk chunk in galaxyChunkSystem.chunkList)
        {
            /*// for each point in each chunk
            foreach (GameObject pointA in chunk.chunkGameObjectList)
            {
                *//*List<GalaxyChunk> chunksToCheck = new List<GalaxyChunk>();
                chunksToCheck.Add(chunk); // adding the point's chunk
                chunksToCheck.AddRange(galaxyChunkSystem.getAdjacentChunks(chunk)); // adding all adjacent chunks (that exist)*//*

                foreach (GameObject pointB in chunk.chunkGameObjectList)
                {
                    if (pointA != pointB && Vector3.Distance(pointA.transform.position, pointB.transform.position) < minDistance)
                    {
                        galaxyChunkSystem.deleteGameObjectFromChunk(pointA, chunk);
                    }
                }
            }*/

            // iterate through every object in a given chunk
            for (int i = 0; i < chunk.chunkGameObjectList.Count;)
            {
                // assume that we dont have to delete the object
                bool shouldDelete = false;

                // iterate again through every object the given chunk
                for (int j = 0; j < chunk.chunkGameObjectList.Count; j++)
                {
                    // if the objects are not one and the same and the distance between them is smaller than the minDistance
                    if (chunk.chunkGameObjectList[i] != chunk.chunkGameObjectList[j] && 
                        Vector3.Distance(chunk.chunkGameObjectList[i].transform.position, chunk.chunkGameObjectList[j].transform.position) < minDistance)
                    {
                        // flag the object that it should be deleted
                        shouldDelete = true;
                        // break out of the loop since we already know the object should be deleted
                        break;
                    }
                }
                // if the object has been flagged for deletion
                if (shouldDelete)
                {
                    // delete the object
                    galaxyChunkSystem.deleteGameObjectFromChunk(chunk.chunkGameObjectList[i], chunk);
                    // continue the loop without incrementing because an item has been removed from current index
                    // so for the next loop we keep the index the same, but in reality the new object that is checked is the next one
                    continue;
                }
                // we can only reach this statement if no object has been deleted, to continue with the iteration
                i++;
            }
        }

    }
}
