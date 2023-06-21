using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GalaxyFactory : MonoBehaviour
{
    private ChunkSystem galaxyChunkSystem;
    private StarFactory starFactory;
    private HomeworldDesignator homeworldDesignator;

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

    [SerializeField] private float minDistanceBetweenPoints;

    [SerializeField] private GameObject testCubePrefabBlue;
    [SerializeField] private GameObject testCubePrefabRed;
    [SerializeField] private GameObject testCubePrefabGreen;

    PointsOnDiskSettings primaryDiskSettings;
    PointsOnDiskSettings secondaryDiskSettings;

    struct PointsOnDiskSettings
    {
        public int numberOfPoints;
        public float turnFraction;
        public float distanceFactor;
        public float locationNoiseXZ;
        public float locationNoiseY;
        public float minLocationNoiseXZ;
        public bool decreaseXNoiseByDistance;
        public bool decreaseYNoiseByDistance;
        public bool decreaseZNoiseByDistance;

        public PointsOnDiskSettings(int numberOfPoints, float turnFraction, float distanceFactor, float locationNoiseXZ, float locationNoiseY, float minLocationNoiseXZ, bool decreaseXNoiseByDistance, bool decreaseYNoiseByDistance, bool decreaseZNoiseByDistance)
        {
            this.numberOfPoints = numberOfPoints;
            this.turnFraction = turnFraction;
            this.distanceFactor = distanceFactor;
            this.locationNoiseXZ = locationNoiseXZ;
            this.locationNoiseY = locationNoiseY;
            this.minLocationNoiseXZ = minLocationNoiseXZ;
            this.decreaseXNoiseByDistance = decreaseXNoiseByDistance;
            this.decreaseYNoiseByDistance = decreaseYNoiseByDistance;
            this.decreaseZNoiseByDistance = decreaseZNoiseByDistance;
        }
    }

    public Text UIText;

    void Start()
    {
        galaxyChunkSystem = GameManagers.chunkSystem;
        starFactory = GameManagers.starFactory;
        homeworldDesignator = GameManagers.homeworldDesignator;


        // i create these structs that hold the settings i need to pass to the disc creating functions
        // because sending in a million parameters is just horrible
        primaryDiskSettings = new PointsOnDiskSettings(
            primaryNumberOfPoints, 
            primaryTurnFraction, 
            primaryDistanceFactor, 
            primaryLocationNoiseXZ, 
            primaryLocationNoiseY, 
            primaryMinLocationNoiseXZ,
            true, true, true);

        secondaryDiskSettings = new PointsOnDiskSettings(
            secondaryNumberOfPoints,
            secondaryTurnFraction,
            secondaryDistanceFactor,
            secondaryLocationNoiseXZ,
            secondaryLocationNoiseY,
            secondaryMinLocationNoiseXZ,
            false, true, false);

        // if the main menu tells us the game started via pressing new game then create a new galaxy
        if (MainMenu.gameStartState == MainMenu.GameStartState.NewGame)
        {
            generateGalaxy();
        }
    }

    public void generateGalaxy()
    {
        // primary formation
        createPointsOnDisk(primaryDiskSettings);
        // secondary formation
        createPointsOnDisk(secondaryDiskSettings);

        // remove overlapped points caused by location noise rng
        removeOverlappedStarSystems(minDistanceBetweenPoints);

        // designate the homeworld
        homeworldDesignator.designateHomeworld();
    }

    private void createPointsOnDisk(PointsOnDiskSettings diskSettings)
    {
        for (int i = 0; i < diskSettings.numberOfPoints; i++)
        {
            // computing points on a circle with a specific turn fraction that forms a galaxy shape
            float distance = i / (diskSettings.numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * diskSettings.turnFraction * i;

            float pointX = distance * Mathf.Cos(angle) * diskSettings.distanceFactor;
            float pointY = 0;
            float pointZ = distance * Mathf.Sin(angle) * diskSettings.distanceFactor;

            float noiseX;
            float noiseY;
            float noiseZ;

            // mapping the distance to center to the inverse of maximum location noise,
            // this way the further away from the center a point is,
            // we can potentially decrease the location noise if required,
            // on any of the 3 axis
            float distanceToCenter = Vector3.Distance(new Vector3(pointX, 0, pointZ), Vector3.zero);
            float mapInput = Mathf.InverseLerp(diskSettings.distanceFactor, 0, distanceToCenter);
            float mapOutputXZ = Mathf.Lerp(0, diskSettings.locationNoiseXZ, mapInput);
            float mapOutputY = Mathf.Lerp(0, diskSettings.locationNoiseY, mapInput);

            // making sure the location noise doesnt reach 0, and is atleast at a minimum threshhold value (0 noise creates bad looking straight line point formations)
            if (mapOutputXZ < diskSettings.minLocationNoiseXZ) mapOutputXZ = diskSettings.minLocationNoiseXZ;

            if (diskSettings.decreaseXNoiseByDistance) noiseX = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseX = Random.Range(-diskSettings.locationNoiseXZ, diskSettings.locationNoiseXZ);

            if (diskSettings.decreaseYNoiseByDistance) noiseY = Random.Range(-mapOutputY, mapOutputY);
            else noiseY = Random.Range(-diskSettings.locationNoiseY, diskSettings.locationNoiseY);

            if (diskSettings.decreaseZNoiseByDistance) noiseZ = Random.Range(-mapOutputXZ, mapOutputXZ);
            else noiseZ = Random.Range(-diskSettings.locationNoiseXZ, diskSettings.locationNoiseXZ);

            float pointXAfterNoise = pointX + noiseX;
            float pointYAfterNoise = pointY + noiseY;
            float pointZAfterNoise = pointZ + noiseZ;

            starFactory.createStarSystem(new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise));
        }
    }

    private void removeOverlappedStarSystems(float minDistance)
    {
        int checksMade = 0;
        // for each chunk
        foreach (Chunk chunk in galaxyChunkSystem.chunkList)
        {
            // iterate through every object in a given chunk
            for (int i = 0; i < chunk.chunkGameObjectList.Count;)
            {
                // assume that we dont have to delete the object
                bool shouldDelete = false;

                // iterate again through every object in the given chunk
                for (int j = 0; j < chunk.chunkGameObjectList.Count; j++)
                {
                    checksMade++;
                    // if the objects are not one and the same and the distance between them is smaller than the minDistance
                    // and if both objects have Star tags
                    if (chunk.chunkGameObjectList[i] != chunk.chunkGameObjectList[j] &&
                        Vector3.Distance(chunk.chunkGameObjectList[i].transform.position, chunk.chunkGameObjectList[j].transform.position) < minDistance &&
                        chunk.chunkGameObjectList[i].tag == "Star System" && chunk.chunkGameObjectList[j].tag == "Star System")
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

        Debug.Log("Checks made: " + checksMade);
    }

    private void createTestCube(GameObject parent)
    {
        GameObject cube;
        cube = Instantiate(testCubePrefabBlue, parent.transform.position, Quaternion.identity);
        cube.transform.parent = parent.transform;
    }
}
