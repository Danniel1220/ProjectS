using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static PlanetShapeSettings;

public class PlanetFactory : MonoBehaviour
{
    private NameGenerator nameGenerator;

    private const int DEFAULT_RESOLUTION = 40;
    private const int DEFAULT_NUMBER_OF_LAYERS = 5;

    private const float MIN_PLANET_RADIUS = 2f;
    private const float MAX_PLANET_RADIUS = 6f;

    // layer 1 random min/max parameter values
    private const bool SHAPE_LAYER1_USE_FIRST_LAYER_AS_MASK = false;
    private const NoiseSettings.FilterType SHAPE_LAYER1_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Simple;
    private const float MIN_SHAPE_LAYER1_STRENGHT = 0.0001f;
    private const float MAX_SHAPE_LAYER1_STRENGHT = 0.1f;
    private const float MIN_SHAPE_LAYER1_BASE_ROUGHNESS = 0.5f;
    private const float MAX_SHAPE_LAYER1_BASE_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER1_ROUGHNESS = 0f;
    private const float MAX_SHAPE_LAYER1_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER1_PERSISTENCE = 0.1f;
    private const float MAX_SHAPE_LAYER1_PERSISTENCE = 0.3f;
    private const float MIN_SHAPE_LAYER1_MIN_VALUE = 0.6f;
    private const float MAX_SHAPE_LAYER1_MIN_VALUE = 1f;

    // layer 2 random min/max parameter values
    private const bool SHAPE_LAYER2_USE_FIRST_LAYER_AS_MASK = true;
    private const NoiseSettings.FilterType SHAPE_LAYER2_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Rigid;
    private const float MIN_SHAPE_LAYER2_STRENGHT = 0f;
    private const float MAX_SHAPE_LAYER2_STRENGHT = 1f;
    private const float MIN_SHAPE_LAYER2_BASE_ROUGHNESS = 1f;
    private const float MAX_SHAPE_LAYER2_BASE_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER2_ROUGHNESS = 0f;
    private const float MAX_SHAPE_LAYER2_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER2_PERSISTENCE = 0.1f;
    private const float MAX_SHAPE_LAYER2_PERSISTENCE = 0.3f;
    private const float MIN_SHAPE_LAYER2_MIN_VALUE = 0f;
    private const float MAX_SHAPE_LAYER2_MIN_VALUE = 0f;
    private const float MIN_SHAPE_LAYER2_WEIGHT_MULTIPLIER = 0f;
    private const float MAX_SHAPE_LAYER2_WEIGHT_MULTIPLIER = 0f;

    // layer 3 random min/max parameter values
    private const bool SHAPE_LAYER3_USE_FIRST_LAYER_AS_MASK = true;
    private const NoiseSettings.FilterType SHAPE_LAYER3_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Rigid;
    private const float MIN_SHAPE_LAYER3_STRENGHT = 0f;
    private const float MAX_SHAPE_LAYER3_STRENGHT = 1f;
    private const float MIN_SHAPE_LAYER3_BASE_ROUGHNESS = 1f;
    private const float MAX_SHAPE_LAYER3_BASE_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER3_ROUGHNESS = 0f;
    private const float MAX_SHAPE_LAYER3_ROUGHNESS = 5f;
    private const float MIN_SHAPE_LAYER3_PERSISTENCE = 0.1f;
    private const float MAX_SHAPE_LAYER3_PERSISTENCE = 0.3f;
    private const float MIN_SHAPE_LAYER3_MIN_VALUE = 0f;
    private const float MAX_SHAPE_LAYER3_MIN_VALUE = 0f;
    private const float MIN_SHAPE_LAYER3_WEIGHT_MULTIPLIER = 0f;
    private const float MAX_SHAPE_LAYER3_WEIGHT_MULTIPLIER = 0f;


    // color biome random min/max parameter values
    private const int MIN_BIOME_NUMBER_OF_KEYS = 2;
    private const int MAX_BIOME_NUMBER_OF_KEYS = 5;

    // color noise settings
    private const NoiseSettings.FilterType COLOR_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Simple;
    private const float MIN_COLOR_NOISE_OFFSET = 0f;
    private const float MAX_COLOR_NOISE_OFFSET = 1f;
    private const float MIN_COLOR_NOISE_STRENGHT = 0.2f;
    private const float MAX_COLOR_NOISE_STRENGHT = 2f;
    private const int COLOR_SIMPLE_NOISE_NUMBER_OF_LAYERS = 2;
    private const float MIN_COLOR_SIMPLE_NOISE_STRENGHT = 0f;
    private const float MAX_COLOR_SIMPLE_NOISE_STRENGHT = 2f;
    private const float MIN_COLOR_SIMPLE_NOISE_BASE_ROUGHNESS = 0.25f;
    private const float MAX_COLOR_SIMPLE_NOISE_BASE_ROUGHNESS = 2f;
    private const float MIN_COLOR_SIMPLE_NOISE_ROUGHNESS = 0f;
    private const float MAX_COLOR_SIMPLE_NOISE_ROUGHNESS = 4f;
    private const float MIN_COLOR_SIMPLE_NOISE_PERSISTENCE = 0f;
    private const float MAX_COLOR_SIMPLE_NOISE_PERSISTENCE = 1f;
    private const float MIN_COLOR_SIMPLE_NOISE_MIN_VALUE = 0f;
    private const float MAX_COLOR_SIMPLE_NOISE_MIN_VALUE = 1f;
    private const float MIN_COLOR_NOISE_BLEND_AMOUNT = 0f;
    private const float MAX_COLOR_NOISE_BLEND_AMOUNT = 1f;
    private const float BIOME_COLOR_NOISE_OFFSET = 0f;
    private const float BIOME_COLOR_NOISE_STRENGHT = 0.33f;
    private const float BIOME_COLOR_BLEND_AMOUNT = 0.66f;
    private const float MIN_BIOME_TINT_PERCENT = 0f;
    private const float MAX_BIOME_TINT_PERCENT = 1f;
    private const float BIOME1_START_HEIGHT = 0f;
    private const float BIOME2_START_HEIGHT = 0f;
    private const float BIOME3_START_HEIGHT = 0.969f;

    private const float PLANET_LIGHT_RANGE = 50f;
    private const float PLANET_LIGHT_OFFSET = 20f;
    private const float PLANET_LIGHT_INTENSITY = 100f;

    private const int MIN_PLANET_AMOUNT = 0;
    private const int MAX_PLANET_AMOUNT = 8;

    private const float MIN_DISTANCE_BETWEEN_PLANET_ORBITS = 32f;

    private const float MIN_PLANET_SPIN = 5f;
    private const float MAX_PLANET_SPIN = 30f;

    private const float MIN_ORBITAL_SPEED = 10f;
    private const float MAX_ORBITAL_SPEED = 40f;

    private const float CLASS_M_STAR_ROCHE_LIMIT = 8f;
    private const float CLASS_K_STAR_ROCHE_LIMIT = 9f;
    private const float CLASS_G_STAR_ROCHE_LIMIT = 10f;
    private const float CLASS_F_STAR_ROCHE_LIMIT = 11f;
    private const float CLASS_A_STAR_ROCHE_LIMIT = 15f;
    private const float CLASS_B_STAR_ROCHE_LIMIT = 20f;
    private const float CLASS_O_STAR_ROCHE_LIMIT = 100f;

    private List<float> classMStarOrbitalDistances = new List<float>();
    private List<float> classKStarOrbitalDistances = new List<float>();
    private List<float> classGStarOrbitalDistances = new List<float>();
    private List<float> classFStarOrbitalDistances = new List<float>();
    private List<float> classAStarOrbitalDistances = new List<float>();
    private List<float> classBStarOrbitalDistances = new List<float>();
    private List<float> classOStarOrbitalDistances = new List<float>();

    private int planetIndexCount = 0;

    void Start()
    {
        nameGenerator = GameManagers.nameGenerator;

        for (int i = 0; i < MAX_PLANET_AMOUNT; i++)
        {
            // first orbit should be equal to the star's roche limit + half of the maximum planet radius
            if (i == 0)
            {
                classMStarOrbitalDistances.Add(CLASS_M_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classKStarOrbitalDistances.Add(CLASS_K_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classGStarOrbitalDistances.Add(CLASS_G_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classFStarOrbitalDistances.Add(CLASS_F_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classAStarOrbitalDistances.Add(CLASS_A_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classBStarOrbitalDistances.Add(CLASS_B_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
                classOStarOrbitalDistances.Add(CLASS_O_STAR_ROCHE_LIMIT + MAX_PLANET_RADIUS / 2);
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

    // this function is used to generate a completely random planet given a parent/target transform and an orbital distance
    // the reason why we receive that orbital distance and not generate it here is because i need to keep track of
    // the other planets in a given system to not overlap them and it'd be overly complicated to implement that here
    public GameObject generatePlanet(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = SHAPE_LAYER1_NOISE_FILTER_TYPE;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = Random.Range(MIN_SHAPE_LAYER1_STRENGHT, MAX_SHAPE_LAYER1_STRENGHT);
        layer1SimpleNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer1SimpleNoiseSettings.baseRoughness = Random.Range(MIN_SHAPE_LAYER1_BASE_ROUGHNESS, MAX_SHAPE_LAYER1_BASE_ROUGHNESS);
        layer1SimpleNoiseSettings.roughness = Random.Range(MIN_SHAPE_LAYER1_ROUGHNESS, MAX_SHAPE_LAYER1_ROUGHNESS);
        layer1SimpleNoiseSettings.persistence = Random.Range(MIN_SHAPE_LAYER1_PERSISTENCE, MAX_SHAPE_LAYER1_PERSISTENCE);
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = Random.Range(MIN_SHAPE_LAYER1_MIN_VALUE, MAX_SHAPE_LAYER1_MIN_VALUE);

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(SHAPE_LAYER1_USE_FIRST_LAYER_AS_MASK, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = SHAPE_LAYER2_NOISE_FILTER_TYPE;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = Random.Range(MIN_SHAPE_LAYER2_STRENGHT, MAX_SHAPE_LAYER2_STRENGHT);
        layer2RigidNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer2RigidNoiseSettings.baseRoughness = Random.Range(MIN_SHAPE_LAYER2_BASE_ROUGHNESS, MAX_SHAPE_LAYER2_BASE_ROUGHNESS);
        layer2RigidNoiseSettings.roughness = Random.Range(MIN_SHAPE_LAYER2_ROUGHNESS, MAX_SHAPE_LAYER2_ROUGHNESS);
        layer2RigidNoiseSettings.persistence = Random.Range(MIN_SHAPE_LAYER2_PERSISTENCE, MAX_SHAPE_LAYER2_PERSISTENCE);
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = Random.Range(MIN_SHAPE_LAYER2_MIN_VALUE, MAX_SHAPE_LAYER2_MIN_VALUE);
        layer2RigidNoiseSettings.weightMultiplier = Random.Range(MIN_SHAPE_LAYER2_WEIGHT_MULTIPLIER, MAX_SHAPE_LAYER2_WEIGHT_MULTIPLIER);

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(SHAPE_LAYER2_USE_FIRST_LAYER_AS_MASK, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = SHAPE_LAYER3_NOISE_FILTER_TYPE;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = Random.Range(MIN_SHAPE_LAYER3_STRENGHT, MAX_SHAPE_LAYER3_STRENGHT);
        layer3RigidNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer3RigidNoiseSettings.baseRoughness = Random.Range(MIN_SHAPE_LAYER3_BASE_ROUGHNESS, MAX_SHAPE_LAYER3_BASE_ROUGHNESS);
        layer3RigidNoiseSettings.roughness = Random.Range(MIN_SHAPE_LAYER3_ROUGHNESS, MAX_SHAPE_LAYER3_ROUGHNESS);
        layer3RigidNoiseSettings.persistence = Random.Range(MIN_SHAPE_LAYER3_PERSISTENCE, MAX_SHAPE_LAYER3_PERSISTENCE);
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = Random.Range(MIN_SHAPE_LAYER3_MIN_VALUE, MAX_SHAPE_LAYER3_MIN_VALUE);
        layer2RigidNoiseSettings.weightMultiplier = Random.Range(MIN_SHAPE_LAYER3_WEIGHT_MULTIPLIER, MAX_SHAPE_LAYER3_WEIGHT_MULTIPLIER);

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(SHAPE_LAYER3_USE_FIRST_LAYER_AS_MASK, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = Random.Range(MIN_PLANET_RADIUS, MAX_PLANET_RADIUS);
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = Random.Range(MIN_COLOR_NOISE_STRENGHT, MAX_COLOR_NOISE_STRENGHT);
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = COLOR_SIMPLE_NOISE_NUMBER_OF_LAYERS;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = Random.Range(MIN_COLOR_SIMPLE_NOISE_BASE_ROUGHNESS, MAX_COLOR_SIMPLE_NOISE_BASE_ROUGHNESS);
        colorNoiseSettings.simpleNoiseSettings.roughness = Random.Range(MIN_COLOR_SIMPLE_NOISE_ROUGHNESS, MAX_COLOR_SIMPLE_NOISE_ROUGHNESS);
        colorNoiseSettings.simpleNoiseSettings.persistence = Random.Range(MIN_COLOR_SIMPLE_NOISE_PERSISTENCE, MAX_COLOR_SIMPLE_NOISE_PERSISTENCE);
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(BIOME1_START_HEIGHT, Random.Range(MIN_BIOME_TINT_PERCENT, MAX_BIOME_TINT_PERCENT));
        biomes[1] = generateRandomBiome(BIOME2_START_HEIGHT, Random.Range(MIN_BIOME_TINT_PERCENT, MAX_BIOME_TINT_PERCENT));
        biomes[2] = generateRandomBiome(BIOME3_START_HEIGHT, Random.Range(MIN_BIOME_TINT_PERCENT, MAX_BIOME_TINT_PERCENT));

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = Random.Range(MIN_COLOR_NOISE_OFFSET, MAX_COLOR_NOISE_OFFSET);
        biomeColorSettings.noiseStenght = Random.Range(MIN_COLOR_NOISE_STRENGHT, MAX_COLOR_NOISE_STRENGHT);
        biomeColorSettings.blendAmount = Random.Range(MIN_COLOR_NOISE_BLEND_AMOUNT, MAX_COLOR_NOISE_BLEND_AMOUNT);
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateRandomGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();
        
        planet.name = nameGenerator.generateRandomName();
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = DEFAULT_RESOLUTION;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        addLightningToPlanet(planet);
        
        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), Random.Range(MIN_ORBITAL_SPEED, MAX_ORBITAL_SPEED), Random.Range(MIN_PLANET_SPIN, MAX_PLANET_SPIN), orbitDistance);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 6f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = planetIndexCount;
        // incrementing the planet index so that each planet generated has an unique index
        planetIndexCount++;

        return planet;
    }

    // this function is more specific and used to generate planets taken from a save file
    public GameObject generatePlanet(Transform targetTransform, PlanetShapeSettings planetshapeSettings, PlanetColorSettings planetColorSettings, string name, float orbitDistance)
    {
        GameObject planet = new GameObject();
        planet.name = name;
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = DEFAULT_RESOLUTION;
        planetGenerationSettings.shapeSettings = planetshapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        addLightningToPlanet(planet);

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), Random.Range(MIN_ORBITAL_SPEED, MAX_ORBITAL_SPEED), Random.Range(MIN_PLANET_SPIN, MAX_PLANET_SPIN), orbitDistance);

        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 6f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = planetIndexCount;
        // incrementing the planet index so that each planet generated has an unique index
        planetIndexCount++;

        return planet;
    }

    private static void addLightningToPlanet(GameObject planet)
    {
        GameObject lightGameObjectNorth = new GameObject("PlanetLightning");
        GameObject lightGameObjectSouth = new GameObject("PlanetLightning");
        GameObject lightGameObjectEast = new GameObject("PlanetLightning");
        GameObject lightGameObjectWest = new GameObject("PlanetLightning");

        Light lightNorth = lightGameObjectNorth.AddComponent<Light>();
        Light lightSouth = lightGameObjectSouth.AddComponent<Light>();
        Light lightEast = lightGameObjectEast.AddComponent<Light>();
        Light lightWest = lightGameObjectWest.AddComponent<Light>();

        lightNorth.range = PLANET_LIGHT_RANGE;
        lightNorth.intensity = PLANET_LIGHT_INTENSITY;
        lightSouth.range = PLANET_LIGHT_RANGE;
        lightSouth.intensity = PLANET_LIGHT_INTENSITY;
        lightEast.range = PLANET_LIGHT_RANGE;
        lightEast.intensity = PLANET_LIGHT_INTENSITY;
        lightWest.range = PLANET_LIGHT_RANGE;
        lightWest.intensity = PLANET_LIGHT_INTENSITY;

        lightGameObjectNorth.transform.parent = planet.transform;
        lightGameObjectSouth.transform.parent = planet.transform;
        lightGameObjectEast.transform.parent = planet.transform;
        lightGameObjectWest.transform.parent = planet.transform;

        lightGameObjectNorth.transform.localPosition = new Vector3(0, 0, PLANET_LIGHT_OFFSET);
        lightGameObjectSouth.transform.localPosition = new Vector3(0, 0, -PLANET_LIGHT_OFFSET);
        lightGameObjectEast.transform.localPosition = new Vector3(PLANET_LIGHT_OFFSET, 0, 0);
        lightGameObjectWest.transform.localPosition = new Vector3(-PLANET_LIGHT_OFFSET, 0, 0);
    }

    private float generateRandomFloat()
    {
        return Random.Range(-12000, 12000);
    }
    
    private PlanetColorSettings.BiomeColorSettings.Biome generateRandomBiome(float biomeStartHeight, float biomeTintPercent)
    {
        Color tintColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);

        return new PlanetColorSettings.BiomeColorSettings.Biome(generateRandomGradient(), tintColor, biomeStartHeight, biomeTintPercent);
    }

    private Gradient generateRandomGradient()
    {
        int numberOfKeys = Random.Range(MIN_BIOME_NUMBER_OF_KEYS, MAX_BIOME_NUMBER_OF_KEYS);
        float distanceBetweenKeys = 1f / numberOfKeys;
        GradientColorKey[] colorKeys = new GradientColorKey[numberOfKeys];
        for (int i = 0; i < numberOfKeys; i++)
        {
            colorKeys[i].color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
            colorKeys[i].time = distanceBetweenKeys * i;
        }

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    public float getMaxPlanetRadius()
    {
        return MAX_PLANET_RADIUS;
    }
}
