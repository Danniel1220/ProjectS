using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Jobs;
using UnityEngine;

public class StarHelper : MonoBehaviour
{
    [SerializeField] private GalaxyChunkSystem galaxyChunkSystem;
    [SerializeField] private GameObject starContainer;

    [SerializeField] private GameObject classMStarPrefab;
    [SerializeField] private GameObject classKStarPrefab;
    [SerializeField] private GameObject classGStarPrefab;
    [SerializeField] private GameObject classFStarPrefab;
    [SerializeField] private GameObject classAStarPrefab;
    [SerializeField] private GameObject classBStarPrefab;
    [SerializeField] private GameObject classOStarPrefab;

    [SerializeField] private int classMStarAmount = 0;
    [SerializeField] private int classKStarAmount = 0;
    [SerializeField] private int classGStarAmount = 0;
    [SerializeField] private int classFStarAmount = 0;
    [SerializeField] private int classAStarAmount = 0;
    [SerializeField] private int classBStarAmount = 0;
    [SerializeField] private int classOStarAmount = 0;
    [SerializeField] private int defaultedStars = 0;

    private string lowerCaseVowels = "aeiou";
    private string upperCaseVowels = "AEIOU";

    private string lowerCaseConsonants = "bcdfghjklmnpqrstvwxyz";
    private string upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

    List<GalaxyChunk> chunks;

    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyManager").GetComponent<GalaxyChunkSystem>();
        chunks = galaxyChunkSystem.getAllChunks();
        starContainer = GameObject.Find("StarContainer");
    }

    public void createStarSystem(Vector3 locationInSpace)
    {
        GameObject star;
        switch (getWeightedRandomStarClass())
        {
            case StarClass.M:
                star = Instantiate(classMStarPrefab, locationInSpace, Quaternion.identity);
                classMStarAmount++;
                break;
            case StarClass.K:
                star = Instantiate(classKStarPrefab, locationInSpace, Quaternion.identity);
                classKStarAmount++;
                break;
            case StarClass.G:
                star = Instantiate(classGStarPrefab, locationInSpace, Quaternion.identity);
                classGStarAmount++;
                break;
            case StarClass.F:
                star = Instantiate(classFStarPrefab, locationInSpace, Quaternion.identity);
                classFStarAmount++;
                break;
            case StarClass.A:
                star = Instantiate(classAStarPrefab, locationInSpace, Quaternion.identity);
                classAStarAmount++;
                break;
            case StarClass.B:
                star = Instantiate(classBStarPrefab, locationInSpace, Quaternion.identity);
                classBStarAmount++;
                break;
            case StarClass.O:
                star = Instantiate(classOStarPrefab, locationInSpace, Quaternion.identity);
                classOStarAmount++;
                break;
            default:
                star = Instantiate(classMStarPrefab, locationInSpace, Quaternion.identity);
                defaultedStars++;
                Debug.LogWarning("Star class RNG error in createStar()... defaulting to class M");
                break;
        }
        star.transform.parent = starContainer.transform;
        star.name = generateRandomStarName();
        star.tag = "Star";
        galaxyChunkSystem.addItemToChunk(star);

        PlanetFactory.generatePlanet(star.transform);
    }

    public void createStarSystem(Vector3 locationInSpace, StarClass starClass)
    {
        GameObject star;
        switch (starClass)
        {
            case StarClass.M:
                star = Instantiate(classMStarPrefab, locationInSpace, Quaternion.identity);
                classMStarAmount++;
                break;
            case StarClass.K:
                star = Instantiate(classKStarPrefab, locationInSpace, Quaternion.identity);
                classKStarAmount++;
                break;
            case StarClass.G:
                star = Instantiate(classGStarPrefab, locationInSpace, Quaternion.identity);
                classGStarAmount++;
                break;
            case StarClass.F:
                star = Instantiate(classFStarPrefab, locationInSpace, Quaternion.identity);
                classFStarAmount++;
                break;
            case StarClass.A:
                star = Instantiate(classAStarPrefab, locationInSpace, Quaternion.identity);
                classAStarAmount++;
                break;
            case StarClass.B:
                star = Instantiate(classBStarPrefab, locationInSpace, Quaternion.identity);
                classBStarAmount++;
                break;
            case StarClass.O:
                star = Instantiate(classOStarPrefab, locationInSpace, Quaternion.identity);
                classOStarAmount++;
                break;
            default:
                star = Instantiate(classMStarPrefab, locationInSpace, Quaternion.identity);
                defaultedStars++;
                Debug.LogWarning("Star class error in createStar() with class argument... defaulting to class M");
                break;
        }
        star.transform.parent = starContainer.transform;
        star.name = generateRandomStarName();
        star.tag = "Star";
        galaxyChunkSystem.addItemToChunk(star);

    }

    private StarClass getWeightedRandomStarClass()
    {
        int starClassRNG = Random.Range(0, 100);
        if (starClassRNG < 40) return StarClass.M; // M class 40% 
        else if (starClassRNG >= 40 && starClassRNG < 60) return StarClass.K; // K class 20%
        else if (starClassRNG >= 60 && starClassRNG < 75) return StarClass.G; // G class 15%
        else if (starClassRNG >= 75 && starClassRNG < 87) return StarClass.F; // F class 12%
        else if (starClassRNG >= 87 && starClassRNG < 97) return StarClass.A; // A class 10%
        else if (starClassRNG >= 97 && starClassRNG < 99) return StarClass.B; // B class 2%
        else if (starClassRNG == 99) return StarClass.O; // O class 1%
        Debug.LogWarning("Star class RNG error in getWeightedRandomStarClass()... defaulting to class M");
        return StarClass.M;
    }

    public string generateRandomStarName()
    {
        string name;
        int nameLenght = Random.Range(4, 9);

        // 50/50 chance of the name starting with a vowel or consonant
        if (Random.Range(0, 2) == 0)
        {
            name = upperCaseVowels[Random.Range(0, upperCaseVowels.Length)].ToString();
            for (int i = 1; i < nameLenght; i++)
            {
                if (i % 2 == 0)
                {
                    name += lowerCaseVowels[Random.Range(0, lowerCaseVowels.Length)].ToString();

                }
                else
                {
                    name += lowerCaseConsonants[Random.Range(0, lowerCaseConsonants.Length)].ToString();
                }
            }
        }
        else
        {
            name = upperCaseConsonants[Random.Range(0, upperCaseConsonants.Length)].ToString();
            for (int i = 1; i < nameLenght; i++)
            {
                if (i % 2 == 1)
                {
                    name += lowerCaseVowels[Random.Range(0, lowerCaseVowels.Length)].ToString();

                }
                else
                {
                    name += lowerCaseConsonants[Random.Range(0, lowerCaseConsonants.Length)].ToString();
                }
            }
        }
        return name;
    }

    public void disableAllStarSystemsButOne(Transform starSystemToNotDisable)
    {
        List<GalaxyChunk> chunks = galaxyChunkSystem.getAllChunks();

        foreach (GalaxyChunk chunk in chunks)
        {
            foreach (GameObject star in chunk.chunkGameObjectList)
            {
                if (star != starSystemToNotDisable.gameObject && star.tag == "Star")
                {
                    star.SetActive(false);
                }
            }
        }
    }

    public void moveStarSystemsRelativeToPoint(Transform centerPoint, bool outwards)
    {
        List<GalaxyChunk> chunks = galaxyChunkSystem.getAllChunks();

        foreach (GalaxyChunk chunk in chunks)
        {
            foreach (GameObject star in chunk.chunkGameObjectList)
            {
                if (star != centerPoint.transform.parent.gameObject && star.tag == "Star")
                {
                    StartCoroutine(moveStarRelativeToPoint(centerPoint.transform, star.transform, outwards));
                }
            }
        }
    }

    public void enableAllStarsSystems()
    {
        List<GalaxyChunk> chunks = galaxyChunkSystem.getAllChunks();

        foreach (GalaxyChunk chunk in chunks)
        {
            foreach (GameObject star in chunk.chunkGameObjectList)
            {
                star.SetActive(true);
            }
        }
    }

    IEnumerator moveStarRelativeToPoint(Transform centerStar, Transform starToMove, bool outwards)
    {
        float movementTime = 1f;
        float strenght = 50f;
        Vector3 direction = (centerStar.position - starToMove.position).normalized;
        if (outwards) direction *= -1;
        while (movementTime > 0)
        {
            starToMove.Translate(direction * strenght, Space.World);
            movementTime -= Time.deltaTime;
            yield return null;
        } 
    }

    private JobHandle moveStarsOutwardsFromCenterPointJob(Transform centerPoint, Transform starToMove)
    {
        moveStarsOutwardsFromCenterPoint job = new moveStarsOutwardsFromCenterPoint();
        job.centerPoint = centerPoint;
        return job.Schedule();
    }
}

public struct moveStarsOutwardsFromCenterPoint : IJob
{
    public Transform centerPoint; 
    public Transform starToMove;

    public void Execute()
    {
        
    }
}
