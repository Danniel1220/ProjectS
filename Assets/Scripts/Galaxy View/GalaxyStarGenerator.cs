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
    [SerializeField] private float primaryMinLocationNoiseXZ;
    [SerializeField] private float primaryLocationNoiseY;

    [SerializeField] private int secondaryNumberOfPoints;
    [SerializeField] private float secondaryTurnFraction;
    [SerializeField] private float secondaryDistanceFactor;
    [SerializeField] private float secondaryLocationNoiseXZ;
    [SerializeField] private float secondaryMinLocationNoiseXZ;
    [SerializeField] private float secondaryLocationNoiseY;

    [SerializeField] private float galaxyMaxDiameter;

    [SerializeField] private float minDistanceBetweenPoints;

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

    private GalaxyChunkSystem galaxyChunkSystem;

    public Text UIText;

    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();

        /*primaryNumberOfPoints = 30000;
        primaryTurnFraction = 0.750016f;
        primaryDistanceFactor = 100000;
        primaryLocationNoiseXZ = 25000;
        primaryLocationNoiseY = 1000f;

        secondaryNumberOfPoints = 10000;
        secondaryTurnFraction = 1.618034f;
        secondaryDistanceFactor = 130000;
        secondaryLocationNoiseXZ = 10000f;
        secondaryLocationNoiseY = 500f;*/

        primaryNumberOfPoints = 7000;
        primaryTurnFraction = 0.7501f;
        primaryDistanceFactor = 100000;
        primaryLocationNoiseXZ = 25000;
        primaryMinLocationNoiseXZ = 4000f;
        primaryLocationNoiseY = 5000f;

        secondaryNumberOfPoints = 3000;
        secondaryTurnFraction = 1.618034f;
        secondaryDistanceFactor = 110000;
        secondaryLocationNoiseXZ = 10000f;
        secondaryMinLocationNoiseXZ = 2500f;
        secondaryLocationNoiseY = 2500f;

        minDistanceBetweenPoints = 1000f;

        // primary formation
        createPointsOnDisk(primaryNumberOfPoints, primaryTurnFraction, primaryDistanceFactor, primaryLocationNoiseXZ, primaryLocationNoiseY, primaryMinLocationNoiseXZ,
            true, true, true);
        // secondary formation
        createPointsOnDisk(secondaryNumberOfPoints, secondaryTurnFraction, secondaryDistanceFactor, secondaryLocationNoiseXZ, secondaryLocationNoiseY, secondaryMinLocationNoiseXZ,
            false, true, false);

        // remove overlapped points caused by location noise rng
        removeOverlappedPointsV2(minDistanceBetweenPoints);

        List<GalaxyChunk> chunkList = galaxyChunkSystem.getAllChunks();

        foreach (GalaxyChunk chunk in chunkList)
        {
            foreach (GameObject point in chunk.chunkGameObjectList)
            {
                createStar(point);
            }
        }
    }

    void Update()
    {

    }

    private void clearAllTestCubes()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("testCube");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
    }

    private void createPointsOnDisk(int numberOfPoints, float turnFraction, float distanceFactor, float locationNoiseXZ, float locationNoiseY, float minLocationNoiseXZ,
        bool decreaseXNoiseByDistance, bool decreaseYNoiseByDistance, bool decreaseZNoiseByDistance)
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

            // making sure the location noise doesnt reach 0, and is atleast at a minimum threshhold value (creates bad looking straight line point formations)
            if (mapOutputXZ < minLocationNoiseXZ) mapOutputXZ = minLocationNoiseXZ;

            if (decreaseXNoiseByDistance) noiseX = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseX = Random.Range(-locationNoiseXZ, locationNoiseXZ);

            if (decreaseYNoiseByDistance) noiseY = Random.Range(-mapOutputY, mapOutputY);
            else noiseY = Random.Range(-locationNoiseY, locationNoiseY);

            if (decreaseZNoiseByDistance) noiseZ = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseZ = Random.Range(-locationNoiseXZ, locationNoiseXZ);

            float pointXAfterNoise = pointX + noiseX;
            float pointYAfterNoise = pointY + noiseY;
            float pointZAfterNoise = pointZ + noiseZ;

            // creating the point in space
            GameObject point = new GameObject();
            point.transform.position = new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise);
            point.transform.rotation = Quaternion.identity;
            point.name = "Star";
            point.tag = "Galaxy View Star";
            galaxyChunkSystem.addItemToChunk(point);
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
            // iterate through every object in a given chunk
            for (int i = 0; i < chunk.chunkGameObjectList.Count;)
            {
                // assume that we dont have to delete the object
                bool shouldDelete = false;

                // iterate again through every object the given chunk
                for (int j = 0; j < chunk.chunkGameObjectList.Count; j++)
                {
                    // if the objects are not one and the same and the distance between them is smaller than the minDistance
                    // and if both object have Galaxy View Star tags
                    if (chunk.chunkGameObjectList[i] != chunk.chunkGameObjectList[j] && 
                        Vector3.Distance(chunk.chunkGameObjectList[i].transform.position, chunk.chunkGameObjectList[j].transform.position) < minDistance &&
                        chunk.chunkGameObjectList[i].tag == "Galaxy View Star" && chunk.chunkGameObjectList[j].tag == "Galaxy View Star")
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

    public void createStar(GameObject parent)
    {
        GameObject star;
        int starClassRNG = Random.Range(0, 100);
        Debug.Log("Creating star...\nRNG: " + starClassRNG);
        if (starClassRNG < 40) // M class 40%
        {
            star = Instantiate(classMStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class M Star");
        }
        else if (starClassRNG >= 40 && starClassRNG < 60) // K class 20%
        {
            star = Instantiate(classKStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class K Star");
        }
        else if (starClassRNG >= 60 && starClassRNG < 75) // G class 15%
        {
            star = Instantiate(classGStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class G Star");
        }
        else if (starClassRNG <= 75 && starClassRNG < 87) // F class 12%
        {
            star = Instantiate(classFStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class F Star");
        }
        else if (starClassRNG >= 87 && starClassRNG < 97) // A class 10%
        {
            star = Instantiate(classAStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class A Star");
        }
        else if (starClassRNG >= 97 && starClassRNG < 99) // B class 2%
        {
            star = Instantiate(classBStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class B Star");
        }
        else // O class 1%
        {
            star = Instantiate(classOStarPrefab, parent.transform.position, Quaternion.identity);
            Debug.Log("Created Class O Star");
        }
        star.transform.parent = parent.transform;
    }

    public void createTestCube(GameObject parent)
    {
        GameObject cube;
        cube = Instantiate(testCubePrefabBlue, parent.transform.position, Quaternion.identity);
        cube.transform.parent = parent.transform;
    }
}
