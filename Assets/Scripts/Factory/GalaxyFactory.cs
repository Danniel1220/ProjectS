using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GalaxyFactory : MonoBehaviour
{
    private ChunkSystem chunkSystem;
    private StarFactory starFactory;

    [SerializeField] private int primaryNumberOfPoints;
    [SerializeField] public float primaryTurnFraction;
    [SerializeField] private float primaryDistanceFactor;
    [SerializeField] private float primaryLocationNoiseXZ;
    [SerializeField] private float primaryMinLocationNoiseXZ;
    [SerializeField] private float primaryLocationNoiseY;

    [SerializeField] private int secondaryNumberOfPoints;
    [SerializeField] public float secondaryTurnFraction;
    [SerializeField] private float secondaryDistanceFactor;
    [SerializeField] private float secondaryLocationNoiseXZ;
    [SerializeField] private float secondaryMinLocationNoiseXZ;
    [SerializeField] private float secondaryLocationNoiseY;

    [SerializeField] private float minDistanceBetweenPoints;

    [SerializeField] private GameObject testCubePrefabBlue;
    [SerializeField] private GameObject testCubePrefabRed;
    [SerializeField] private GameObject testCubePrefabGreen;

    public PointsOnDiskSettings primaryDiskSettings;
    public PointsOnDiskSettings secondaryDiskSettings;

    public int numberOfStarsGenerated;
    private IEnumerator primaryDiskCoroutine;
    private IEnumerator secondaryDiskCoroutine;
    private IEnumerator removeOverlappedStarSystemsCoroutine;

    private bool primaryDiskGeneratedFlag;
    private bool secondaryDiskGeneratedFlag;

    public enum DiskType { primary, secondary };

    private DateTime galaxyGenerationTimeStart;
    private DateTime galaxyGenerationTimeEnd;
    private TimeSpan galaxyGenerationTimeElapsed;

    public struct PointsOnDiskSettings
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
        public DiskType diskType;

        public PointsOnDiskSettings(int numberOfPoints, float turnFraction, float distanceFactor, float locationNoiseXZ, float locationNoiseY, float minLocationNoiseXZ, bool decreaseXNoiseByDistance, bool decreaseYNoiseByDistance, bool decreaseZNoiseByDistance, DiskType diskType)
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
            this.diskType = diskType;
        }
    }

    void Start()
    {
        chunkSystem = GameManagers.chunkSystem;
        starFactory = GameManagers.starFactory;

        galaxyGenerationTimeStart = DateTime.Now;

        // i create these structs that hold the settings i need to pass to the disc creating functions
        // because sending in a million parameters is just horrible
        primaryDiskSettings = new PointsOnDiskSettings(
            primaryNumberOfPoints,
            primaryTurnFraction,
            primaryDistanceFactor,
            primaryLocationNoiseXZ,
            primaryLocationNoiseY,
            primaryMinLocationNoiseXZ,
            true, true, true,
            DiskType.primary);

        secondaryDiskSettings = new PointsOnDiskSettings(
            secondaryNumberOfPoints,
            secondaryTurnFraction,
            secondaryDistanceFactor,
            secondaryLocationNoiseXZ,
            secondaryLocationNoiseY,
            secondaryMinLocationNoiseXZ,
            false, true, false,
            DiskType.secondary);

        primaryDiskGeneratedFlag = false;
        secondaryDiskGeneratedFlag = false;
    }

    public void generateGalaxy()
    {
        primaryDiskCoroutine = createPointsOnDiskEnumerator(primaryDiskSettings);
        secondaryDiskCoroutine = createPointsOnDiskEnumerator(secondaryDiskSettings);
        removeOverlappedStarSystemsCoroutine = removeOverlappedStarSystemsEnumerator(minDistanceBetweenPoints);

        StartCoroutine(primaryDiskCoroutine);
        StartCoroutine(secondaryDiskCoroutine);
        StartCoroutine(removeOverlappedStarSystemsCoroutine);


        //// primary formation
        //createPointsOnDisk(primaryDiskSettings);
        //// secondary formation
        //createPointsOnDisk(secondaryDiskSettings);



        //// remove overlapped points caused by location noise rng
        //removeOverlappedStarSystems(minDistanceBetweenPoints);

        // designate the homeworld
        //homeworldDesignator.designateHomeworld();
    }

    public IEnumerator createPointsOnDiskEnumerator(PointsOnDiskSettings diskSettings)
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
            // we can decrease the location noise on any of the 3 axis, if required
            float distanceToCenter = Vector3.Distance(new Vector3(pointX, 0, pointZ), Vector3.zero);
            float mapInput = Mathf.InverseLerp(diskSettings.distanceFactor, 0, distanceToCenter);
            float mapOutputXZ = Mathf.Lerp(0, diskSettings.locationNoiseXZ, mapInput);
            float mapOutputY = Mathf.Lerp(0, diskSettings.locationNoiseY, mapInput);

            // making sure the location noise doesnt reach 0, and is atleast at a minimum threshhold value (0 noise creates bad looking straight line point formations)
            if (mapOutputXZ < diskSettings.minLocationNoiseXZ) mapOutputXZ = diskSettings.minLocationNoiseXZ;

            // this decreases the location noise by distance, if required
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

            // yield only every 100 stars
            if (i % 100 == 0) yield return null;
        }

        // settings flags to let the factory script know when the disks are done generating
        if (diskSettings.diskType == DiskType.primary) { Debug.Log("[debug] set primary flag to true"); primaryDiskGeneratedFlag = true; }
        if (diskSettings.diskType == DiskType.secondary) { Debug.Log("[debug] set secondary flag to true"); secondaryDiskGeneratedFlag = true; }
    }

    public IEnumerator removeOverlappedStarSystemsEnumerator(float minDistance)
    {
        // skip removing star systems while both flags are false, meaning the galaxy is not done generating yet
        while (primaryDiskGeneratedFlag == false || secondaryDiskGeneratedFlag == false) yield return null;

        int checksMade = 0;
        // for each chunk
        foreach (Chunk chunk in chunkSystem.chunkList)
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
                    chunkSystem.deleteGameObjectFromChunk(chunk.chunkGameObjectList[i], chunk);
                    // continue the loop without incrementing because an item has been removed from current index
                    // so for the next loop we keep the index the same, but in reality the new object that is checked is the next one
                    continue;
                }
                // we can only reach this statement if no object has been deleted, to continue with the iteration
                i++;
            }

            // yield every chunk
            yield return null;
        }

        Debug.Log("Checks made: " + checksMade);

        galaxyGenerationTimeEnd = DateTime.Now;
        galaxyGenerationTimeElapsed = galaxyGenerationTimeEnd - galaxyGenerationTimeStart;

        Debug.Log("Galaxy Generation Time Elapsed: " + galaxyGenerationTimeElapsed);
    }

    public void createPointsOnDisk(PointsOnDiskSettings diskSettings)
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

            chunkSystem.updateDebugStarNumber();
        }
    }

    public void removeOverlappedStarSystems(float minDistance)
    {
        int checksMade = 0;
        // for each chunk
        foreach (Chunk chunk in chunkSystem.chunkList)
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
                    chunkSystem.deleteGameObjectFromChunk(chunk.chunkGameObjectList[i], chunk);
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

    public void createTestCube(GameObject parent, string color, Vector3 position)
    {
        GameObject cube;
        if (color == "blue")
        {
            cube = Instantiate(testCubePrefabBlue, position, Quaternion.identity);
        }
        else if (color == "red")
        {
            cube = Instantiate(testCubePrefabRed, position, Quaternion.identity);
        }
        else
        {
            cube = Instantiate(testCubePrefabGreen, position, Quaternion.identity);
        }
        cube.transform.parent = parent.transform;
        cube.transform.localScale = Vector3.one;
        cube.transform.position = position;
    }

    public void createTestCubesOnDisk(PointsOnDiskSettings diskSettings, bool noise)
    {
        for (int i = 0; i < diskSettings.numberOfPoints; i++)
        {
            // computing points on a circle with a specific turn fraction that forms a galaxy shape
            float distance = i / (diskSettings.numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * diskSettings.turnFraction * i;

            float pointX = distance * Mathf.Cos(angle) * diskSettings.distanceFactor;
            float pointY = 0;
            float pointZ = distance * Mathf.Sin(angle) * diskSettings.distanceFactor;

            if (noise)
            {
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

                if (i % 2 == 0)
                {
                    createTestCube(this.gameObject, "blue", new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise));
                }
                else
                {
                    createTestCube(this.gameObject, "red", new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise));
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    createTestCube(this.gameObject, "blue", new Vector3(pointX, pointY, pointZ));
                }
                else
                {
                    createTestCube(this.gameObject, "red", new Vector3(pointX, pointY, pointZ));
                }
            }
        }
    }

    public void createTestCubesOnDiskGreen(PointsOnDiskSettings diskSettings, bool noise)
    {
        for (int i = 0; i < diskSettings.numberOfPoints; i++)
        {
            // computing points on a circle with a specific turn fraction that forms a galaxy shape
            float distance = i / (diskSettings.numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * diskSettings.turnFraction * i;

            float pointX = distance * Mathf.Cos(angle) * diskSettings.distanceFactor;
            float pointY = 0;
            float pointZ = distance * Mathf.Sin(angle) * diskSettings.distanceFactor;

            if (noise)
            {
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

                createTestCube(this.gameObject, "green", new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise));
            }
            else
            {
                createTestCube(this.gameObject, "green", new Vector3(pointX, pointY, pointZ));
            }
        }
    }

    public void setDiskSettings()
    {
        // i create these structs that hold the settings i need to pass to the disc creating functions
        // because sending in a million parameters is just horrible
        primaryDiskSettings = new PointsOnDiskSettings(
            primaryNumberOfPoints,
            primaryTurnFraction,
            primaryDistanceFactor,
            primaryLocationNoiseXZ,
            primaryLocationNoiseY,
            primaryMinLocationNoiseXZ,
            true, true, true,
            DiskType.primary);

        secondaryDiskSettings = new PointsOnDiskSettings(
            secondaryNumberOfPoints,
            secondaryTurnFraction,
            secondaryDistanceFactor,
            secondaryLocationNoiseXZ,
            secondaryLocationNoiseY,
            secondaryMinLocationNoiseXZ,
            false, true, false,
            DiskType.secondary);
    }
}
