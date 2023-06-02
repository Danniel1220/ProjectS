using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Jobs;
using UnityEngine;

public class StarFactory : MonoBehaviour
{
    private GameObject starSystemsContainer;

    private ChunkSystem galaxyChunkSystem;
    private PlanetFactory planetFactory;

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

    private const string lowerCaseVowels = "aeiou";
    private const string upperCaseVowels = "AEIOU";

    private const string lowerCaseConsonants = "bcdfghjklmnpqrstvwxyz";
    private const string upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

    private const int MIN_PLANET_AMOUNT = 0;
    private const int MAX_PLANET_AMOUNT = 8;

    private const float CLASS_M_STAR_ROCHE_LIMIT = 8f;
    private const float CLASS_K_STAR_ROCHE_LIMIT = 9f;
    private const float CLASS_G_STAR_ROCHE_LIMIT = 10f;
    private const float CLASS_F_STAR_ROCHE_LIMIT = 11f;
    private const float CLASS_A_STAR_ROCHE_LIMIT = 15f;
    private const float CLASS_B_STAR_ROCHE_LIMIT = 20f;
    private const float CLASS_O_STAR_ROCHE_LIMIT = 100f;

    private const float MIN_DISTANCE_BETWEEN_PLANET_ORBITS = 32f;

    private List<float> classMStarOrbitalDistances = new List<float>();
    private List<float> classKStarOrbitalDistances = new List<float>();
    private List<float> classGStarOrbitalDistances = new List<float>();
    private List<float> classFStarOrbitalDistances = new List<float>();
    private List<float> classAStarOrbitalDistances = new List<float>();
    private List<float> classBStarOrbitalDistances = new List<float>();
    private List<float> classOStarOrbitalDistances = new List<float>();

    private int starIndexCounter = 0;

    void Start()
    {
        starSystemsContainer = GameManagers.starSystemsContainer;

        galaxyChunkSystem = GameManagers.chunkSystem;
        planetFactory = GameManagers.planetFactory;

        float maxPlanetRadius = planetFactory.getMaxPlanetRadius();

        for (int i = 0; i < MAX_PLANET_AMOUNT; i++)
        {
            // first orbit should be equal to the star's roche limit + half of the maximum planet radius
            if (i == 0)
            {
                classMStarOrbitalDistances.Add(CLASS_M_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classKStarOrbitalDistances.Add(CLASS_K_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classGStarOrbitalDistances.Add(CLASS_G_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classFStarOrbitalDistances.Add(CLASS_F_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classAStarOrbitalDistances.Add(CLASS_A_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classBStarOrbitalDistances.Add(CLASS_B_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
                classOStarOrbitalDistances.Add(CLASS_O_STAR_ROCHE_LIMIT + maxPlanetRadius / 2);
            }
            else
            {
                classMStarOrbitalDistances.Add(classMStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classKStarOrbitalDistances.Add(classKStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classGStarOrbitalDistances.Add(classGStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classFStarOrbitalDistances.Add(classFStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classAStarOrbitalDistances.Add(classAStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classBStarOrbitalDistances.Add(classBStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
                classOStarOrbitalDistances.Add(classOStarOrbitalDistances.Last() + MIN_DISTANCE_BETWEEN_PLANET_ORBITS);
            }
        }
    }

    // this function is used when creating a fully random star when generating a new galaxy
    // no need for any arguments but the location calculated when creating the points
    public GameObject createStarSystem(Vector3 locationInSpace)
    {
        // creating the new star system's container
        GameObject starSystemContainer = new GameObject();
        StarSystem starSystemScript = starSystemContainer.AddComponent<StarSystem>();
        // assigning an unique index to the star system so it can be later be refferenced via index when looking for it
        starSystemScript.index = starIndexCounter;
        // incrementing the index so the next star system created has a different unique one
        starIndexCounter++;
        // assume that, by default, the star system is not a homeworld
        starSystemScript.isHomeworld = false;

        string starSystemName = generateRandomName();

        starSystemContainer.transform.position = locationInSpace;
        starSystemContainer.name = starSystemName + " System";
        starSystemContainer.tag = "Star System";

        // add the new star system container to the container for all star systems
        starSystemContainer.transform.parent = starSystemsContainer.transform;

        GameObject star;
        // figuring out what star class the new star will be
        switch (getWeightedRandomStarClass())
        {
            case StarClass.M:
                star = Instantiate(classMStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classMStarAmount++;
                break;
            case StarClass.K:
                star = Instantiate(classKStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classKStarAmount++;
                break;
            case StarClass.G:
                star = Instantiate(classGStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classGStarAmount++;
                break;
            case StarClass.F:
                star = Instantiate(classFStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classFStarAmount++;
                break;
            case StarClass.A:
                star = Instantiate(classAStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classAStarAmount++;
                break;
            case StarClass.B:
                star = Instantiate(classBStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classBStarAmount++;
                break;
            case StarClass.O:
                star = Instantiate(classOStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classOStarAmount++;
                break;
            default:
                star = Instantiate(classMStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                defaultedStars++;
                Debug.LogWarning("Star class RNG error in createStar()... defaulting to class M");
                break; 
        }
        // add the new star to the new star system's container
        star.transform.parent = starSystemContainer.transform;
        star.name = starSystemName;
        star.tag = "Star";

        int numberOfPlanets = Random.Range(MIN_PLANET_AMOUNT, MAX_PLANET_AMOUNT);
        if (numberOfPlanets > 0)
        {
            List<float> planetOrbitalDistances = new List<float>(classOStarOrbitalDistances);
            for (int i = 0; i < numberOfPlanets; i++)
            {
                int randomOrbitalDistanceIndex = Random.Range(0, planetOrbitalDistances.Count() - 1);
                float randomOrbitalDistanceValue = planetOrbitalDistances.ElementAt(randomOrbitalDistanceIndex);
                planetOrbitalDistances.RemoveAt(randomOrbitalDistanceIndex);

                GameObject planet = planetFactory.generatePlanet(starSystemContainer.transform, randomOrbitalDistanceValue);
            }
        }

        galaxyChunkSystem.addItemToChunk(starSystemContainer);

        return starSystemContainer;
    }

    // this function is used when creating a star system loaded from a save file
    // since we need to create a specific type of star more arguments are required
    public GameObject createStarSystem(Vector3 locationInSpace, StarClass starClass, string name, bool isHomeworld, int index)
    {
        // creating the new star system's container
        GameObject starSystemContainer = new GameObject();
        StarSystem starSystemScript = starSystemContainer.AddComponent<StarSystem>();
        starSystemScript.index = index;
        starSystemScript.isHomeworld = isHomeworld;

        string starSystemName = name;

        starSystemContainer.transform.position = locationInSpace;
        starSystemContainer.name = starSystemName + " System";
        starSystemContainer.tag = "Star System";

        // add the new star system container to the container for all star systems
        starSystemContainer.transform.parent = starSystemsContainer.transform;

        GameObject star;
        // figuring out what star class the new star will be
        switch (starClass)
        {
            case StarClass.M:
                star = Instantiate(classMStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classMStarAmount++;
                break;
            case StarClass.K:
                star = Instantiate(classKStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classKStarAmount++;
                break;
            case StarClass.G:
                star = Instantiate(classGStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classGStarAmount++;
                break;
            case StarClass.F:
                star = Instantiate(classFStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classFStarAmount++;
                break;
            case StarClass.A:
                star = Instantiate(classAStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classAStarAmount++;
                break;
            case StarClass.B:
                star = Instantiate(classBStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classBStarAmount++;
                break;
            case StarClass.O:
                star = Instantiate(classOStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                classOStarAmount++;
                break;
            default:
                star = Instantiate(classMStarPrefab, starSystemContainer.transform.position, Quaternion.identity);
                defaultedStars++;
                Debug.LogWarning("Star class RNG error in createStar()... defaulting to class M");
                break;
        }
        // add the new star to the new star system's container
        star.transform.parent = starSystemContainer.transform;
        star.name = starSystemName;
        star.tag = "Star";

        galaxyChunkSystem.addItemToChunk(starSystemContainer);

        return starSystemContainer;
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

    public string generateRandomName()
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

    public void disableAllStarSystemsButOne(GameObject starSystemToNotDisable)
    {
        List<Chunk> chunks = galaxyChunkSystem.getAllChunks();

        // disabling all star systems that shouldnt be in view
        foreach (Chunk chunk in chunks)
        {
            foreach (GameObject starSystem in chunk.chunkGameObjectList)
            {
                if (starSystem != starSystemToNotDisable.gameObject && starSystem.tag == "Star System")
                {
                    starSystem.SetActive(false);
                }
            }
        }

        // enabling the current visible star system's planets
        foreach (Transform child in starSystemToNotDisable.transform)
        {
            if (child.gameObject.tag == "Planet")
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void moveStarSystemsRelativeToPoint(GameObject centerPoint, bool outwards)
    {
        List<Chunk> chunks = galaxyChunkSystem.getAllChunks();

        foreach (Chunk chunk in chunks)
        {
            foreach (GameObject star in chunk.chunkGameObjectList)
            {
                if (star != centerPoint && star.tag == "Star")
                {
                    StartCoroutine(moveStarRelativeToPoint(centerPoint.transform, star.transform, outwards));
                }
            }
        }
    }

    public void enableAllStarsSystems()
    {
        List<Chunk> chunks = galaxyChunkSystem.getAllChunks();

        foreach (Chunk chunk in chunks)
        {
            foreach (GameObject starSystem in chunk.chunkGameObjectList)
            {
                starSystem.SetActive(true);
                
                // enabling the star system means the planets shouldnt be visible anymore
                foreach (Transform child in starSystem.transform)
                {
                    if (child.gameObject.tag == "Planet")
                    {
                        child.gameObject.SetActive(false);
                    }
                }
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
