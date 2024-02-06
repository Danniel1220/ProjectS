using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static GalaxyFactory;
using Random = UnityEngine.Random;

public class GalaxyFactory : MonoBehaviour
{
    private ChunkSystem chunkSystem;
    private StarFactory starFactory;

    [SerializeField] private List<DiskSettings> diskSettings;
    [SerializeField] private float minDistanceBetweenPoints;

    [SerializeField] private GameObject testCubePrefab;

    [SerializeField] public Material testCubeMaterialRed;
    [SerializeField] public Material testCubeMaterialOrange;
    [SerializeField] public Material testCubeMaterialYellow;
    [SerializeField] public Material testCubeMaterialGreen;
    [SerializeField] public Material testCubeMaterialBlue;
    [SerializeField] public Material testCubeMaterialPurple;
    [SerializeField] public Material testCubeMaterialWhite;

    public float testCubeLocalScale;

    private IEnumerator primaryDiskCoroutine;
    private IEnumerator secondaryDiskCoroutine;
    private IEnumerator removeOverlappedStarSystemsCoroutine;

    private DateTime galaxyGenerationTimeStart;
    private DateTime galaxyGenerationTimeEnd;
    private TimeSpan galaxyGenerationTimeElapsed;

    public enum CubeColor { Red, Orange, Yellow, Green, Blue, Purple, White }

    [System.Serializable]
    public struct DiskSettings
    {
        public int numberOfPoints;
        public float turnFraction;
        public float turnOffset;
        public float distanceFactor;
        public float locationNoiseX;
        public float locationNoiseY;
        public float locationNoiseZ;
        public float minLocationNoiseX;
        public float minLocationNoiseY;
        public float minLocationNoiseZ;
        public bool decreaseXZNoiseByDistance;
        public bool decreaseYNoiseByDistance;
        public CubeColor cubeColor;

        public DiskSettings(int numberOfPoints, float turnFraction, float turnOffset, float distanceFactor, float locationNoiseX, float locationNoiseY, float locationNoiseZ, float minLocationNoiseX, float minLocationNoiseY, float minLocationNoiseZ, bool decreaseXZNoiseByDistance, bool decreaseYNoiseByDistance, CubeColor cubeColor)
        {
            this.numberOfPoints = numberOfPoints;
            this.turnFraction = turnFraction;
            this.turnOffset = turnOffset;
            this.distanceFactor = distanceFactor;
            this.locationNoiseX = locationNoiseX;
            this.locationNoiseY = locationNoiseY;
            this.locationNoiseZ = locationNoiseZ;
            this.minLocationNoiseX = minLocationNoiseX;
            this.minLocationNoiseY = minLocationNoiseY;
            this.minLocationNoiseZ = minLocationNoiseZ;
            this.decreaseXZNoiseByDistance = decreaseXZNoiseByDistance;
            this.decreaseYNoiseByDistance = decreaseYNoiseByDistance;
            this.cubeColor = cubeColor;
        }
    }

    void Start()
    {
        chunkSystem = GameManagers.chunkSystem;
        starFactory = GameManagers.starFactory;

        galaxyGenerationTimeStart = DateTime.Now;
    }

    public void generateGalaxy()
    {
        //primaryDiskCoroutine = createPointsOnDiskEnumerator(primaryDiskSettings);
        //secondaryDiskCoroutine = createPointsOnDiskEnumerator(secondaryDiskSettings);
        //removeOverlappedStarSystemsCoroutine = removeOverlappedStarSystemsEnumerator(minDistanceBetweenPoints);

        //StartCoroutine(primaryDiskCoroutine);
        //StartCoroutine(secondaryDiskCoroutine);
        //StartCoroutine(removeOverlappedStarSystemsCoroutine);
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

    public void generateCubesGalaxy(List<DiskSettings> diskSettings)
    {
        // for each disk inside of the list
        foreach(DiskSettings disk in diskSettings)
        {
            generateCubesDisk(disk);
        }
    }

    public void generateCubesDisk(DiskSettings disk)
    {
        for (int i = 0; i < disk.numberOfPoints; i++)
        {
            // computing points on a circle with a specific turn fraction that forms a galaxy shape
            float distance = i / (disk.numberOfPoints - 1f);
            float angle = 2 * Mathf.PI * disk.turnFraction * i;
            angle += disk.turnOffset;

            float pointX = distance * Mathf.Cos(angle) * disk.distanceFactor;
            float pointY = 0;
            float pointZ = distance * Mathf.Sin(angle) * disk.distanceFactor;

            //float pointX = distance * (Mathf.Cos(angle) + disk.turnFraction) * disk.distanceFactor;
            //float pointY = 0;
            //float pointZ = distance * (Mathf.Sin(angle) + disk.turnFraction) * disk.distanceFactor;

            float noiseX;
            float noiseY;
            float noiseZ;

            // mapping the distance to center to the inverse of maximum location noise,
            // this way the further away from the center a point is,
            // we can potentially decrease the location noise if required,
            // on any of the 3 axis
            float distanceToCenter = Vector3.Distance(new Vector3(pointX, 0, pointZ), Vector3.zero);
            float mapInput = Mathf.InverseLerp(disk.distanceFactor, 0, distanceToCenter);
            float mapOutputX = Mathf.Lerp(0, disk.locationNoiseX, mapInput);
            float mapOutputY = Mathf.Lerp(0, disk.locationNoiseY, mapInput);
            float mapOutputZ = Mathf.Lerp(0, disk.locationNoiseZ, mapInput);


            // clamping noises above a set parameter
            if (mapOutputX < disk.minLocationNoiseX) mapOutputX = disk.minLocationNoiseX;
            if (mapOutputY < disk.minLocationNoiseY) mapOutputY = disk.minLocationNoiseY;
            if (mapOutputZ < disk.minLocationNoiseZ) mapOutputZ = disk.minLocationNoiseZ;

            if (disk.decreaseXZNoiseByDistance)
            {
                noiseX = Random.Range(-mapOutputX, mapOutputZ);
            }
            else
            {
                noiseX = Random.Range(-disk.locationNoiseX, disk.locationNoiseX);
            }

            if (disk.decreaseYNoiseByDistance)
            {
                noiseY = Random.Range(-mapOutputY, mapOutputY);
            }
            else
            {
                noiseY = Random.Range(-disk.locationNoiseY, disk.locationNoiseY);
            }

            if (disk.decreaseXZNoiseByDistance)
            {
                noiseZ = Random.Range(-mapOutputZ, mapOutputZ);
            }
            else
            {
                noiseZ = Random.Range(-disk.locationNoiseZ, disk.locationNoiseZ);
            }

            float pointXAfterNoise = pointX + noiseX;
            float pointYAfterNoise = pointY + noiseY;
            float pointZAfterNoise = pointZ + noiseZ;
            
            instantiateTestCube(this.gameObject, disk.cubeColor, new Vector3(pointXAfterNoise, pointYAfterNoise, pointZAfterNoise), testCubeLocalScale);
        }
    }

    public void instantiateTestCube(GameObject parent, CubeColor color, Vector3 position, float localScale)
    {
        GameObject cube;
        cube = Instantiate(testCubePrefab, position, Quaternion.identity);

        switch (color)
        {
            case CubeColor.Red:
                cube.GetComponent<Renderer>().material = testCubeMaterialRed;
                break;
            case CubeColor.Orange:
                cube.GetComponent<Renderer>().material = testCubeMaterialOrange;
                break;
            case CubeColor.Yellow:
                cube.GetComponent<Renderer>().material = testCubeMaterialYellow;
                break;
            case CubeColor.Green:
                cube.GetComponent<Renderer>().material = testCubeMaterialGreen;
                break;
            case CubeColor.Blue:
                cube.GetComponent<Renderer>().material = testCubeMaterialBlue;
                break;
            case CubeColor.Purple:
                cube.GetComponent<Renderer>().material = testCubeMaterialPurple;
                break;
            case CubeColor.White:
                cube.GetComponent<Renderer>().material = testCubeMaterialWhite;
                break;
            
        }

        cube.transform.parent = parent.transform;
        cube.transform.localScale = new Vector3(localScale, localScale, localScale);
        cube.transform.position = position;
    }

    public List<DiskSettings> getDiskSettings()
    {
        return diskSettings;
    }
}
