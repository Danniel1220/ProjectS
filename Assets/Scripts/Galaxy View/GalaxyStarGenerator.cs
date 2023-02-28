using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

    [SerializeField] private int nebulaNumberOfPoints;
    [SerializeField] private float nebulaTurnFraction;
    [SerializeField] private float nebulaDistanceFactor;
    [SerializeField] private float nebulaLocationNoiseXZ;
    [SerializeField] private float nebulaMinLocationNoiseXZ;
    [SerializeField] private float nebulaLocationNoiseY;

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

    private List<Vector3> starSystemPointsInSpace;

    private GalaxyChunkSystem galaxyChunkSystem;
    private StarHelper starHelper;

    public Text UIText;

    public float starScale;

    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyManager").GetComponent<GalaxyChunkSystem>();
        starHelper = GameObject.Find("StarManager").GetComponent<StarHelper>();

/*        primaryNumberOfPoints = 7000;
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

        minDistanceBetweenPoints = 1000f;*/

        // primary formation
        createPointsOnDisk(primaryNumberOfPoints, primaryTurnFraction, primaryDistanceFactor, primaryLocationNoiseXZ, primaryLocationNoiseY, primaryMinLocationNoiseXZ,
            true, true, true);
        // secondary formation
        createPointsOnDisk(secondaryNumberOfPoints, secondaryTurnFraction, secondaryDistanceFactor, secondaryLocationNoiseXZ, secondaryLocationNoiseY, secondaryMinLocationNoiseXZ,
            false, true, false);

        // remove overlapped points caused by location noise rng
        removeOverlappedStarSystems(minDistanceBetweenPoints);
    }

    void Update()
    {

    }

    private void clearAllTestCubes()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("testCube");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj.transform.parent);
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

            starHelper.createStarSystem(new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise));
        }
    }

    private void removeOverlappedStarSystems(float minDistance)
    {
        // for each chunk
        foreach (GalaxyChunk chunk in galaxyChunkSystem.chunkList)
        {
            // iterate through every object in a given chunk
            for (int i = 0; i < chunk.chunkGameObjectList.Count;)
            {
                // assume that we dont have to delete the object
                bool shouldDelete = false;

                // iterate again through every object in the given chunk
                for (int j = 0; j < chunk.chunkGameObjectList.Count; j++)
                {
                    // if the objects are not one and the same and the distance between them is smaller than the minDistance
                    // and if both objects have Galaxy View Star tags
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

    public void createTestCube(GameObject parent)
    {
        GameObject cube;
        cube = Instantiate(testCubePrefabBlue, parent.transform.position, Quaternion.identity);
        cube.transform.parent = parent.transform;
    }
}
