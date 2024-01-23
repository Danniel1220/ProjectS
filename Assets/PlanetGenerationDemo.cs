using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlanetShapeSettings;

public class PlanetGenerationDemo : MonoBehaviour
{
    public bool spinEnabled = false;

    [Header("Biomes Demo Planet")]
    public bool spawnBiomeDemoPlanet = false;

    [Header("Earth Like Planet Without Biomes")]
    public bool spawnEarthLikePlanetWithoutBiomes = false;

    [Header("Earth Like Planet")]
    public bool spawnEarthLikePlanet = false;

    [Header("Earth Like Planet With Random Land Colors")]
    public bool spawnEarthLikePlanetWithRandomLandColors = false;

    [Header("Mars Like Planet")]
    public bool spawnMarsLikePlanet = false;


    private int lastPlanetGeneratedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        generateEarthLikePlanetWithRandomEarthColors(this.transform, 0f);
        this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
        this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
        this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
        this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
        this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
    }

    void Update()
    {
        if (spinEnabled) this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 20f;
        else this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;

        if (spawnBiomeDemoPlanet)
        {
            spawnBiomeDemoPlanet = false;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(false);
            lastPlanetGeneratedIndex++;

            generateBiomeDemoPlanet(this.transform, 0f);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
        }

        if (spawnEarthLikePlanetWithoutBiomes)
        {
            spawnEarthLikePlanetWithoutBiomes = false;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(false);
            lastPlanetGeneratedIndex++;

            generateEarthLikePlanetWithoutBiomes(this.transform, 0f);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
        }

        if (spawnEarthLikePlanetWithRandomLandColors)
        {
            spawnEarthLikePlanetWithRandomLandColors = false;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(false);
            lastPlanetGeneratedIndex++;

            generateEarthLikePlanetWithRandomEarthColors(this.transform, 0f);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
        }

        if (spawnEarthLikePlanet)
        {
            spawnEarthLikePlanet = false;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(false);
            lastPlanetGeneratedIndex++;

            generateEarthLikePlanet(this.transform, 0f);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
        }

        if (spawnMarsLikePlanet)
        {
            spawnMarsLikePlanet = false;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(false);
            lastPlanetGeneratedIndex++;

            generateMarsLikePlanet(this.transform, 0f);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.SetActive(true);
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<PlanetGenerationSettings>().shapeSettings.planetRadius = 400f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitDistance = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().rotationSpeed = 0f;
            this.transform.GetChild(lastPlanetGeneratedIndex).gameObject.GetComponent<Orbit>().orbitSpeed = 0f;
        }
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
        int numberOfKeys = 5;
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

    private Gradient generateEarthLikeOceanGradient()
    {
        int numberOfKeys = 5;
        GradientColorKey[] colorKeys = new GradientColorKey[numberOfKeys];
        ColorUtility.TryParseHtmlString("#011749", out colorKeys[0].color);
        ColorUtility.TryParseHtmlString("#142E6D", out colorKeys[1].color);
        ColorUtility.TryParseHtmlString("#1F4C91", out colorKeys[2].color);
        ColorUtility.TryParseHtmlString("#085496", out colorKeys[3].color);
        ColorUtility.TryParseHtmlString("#2F90C2", out colorKeys[4].color);
        colorKeys[0].time = 0f;
        colorKeys[1].time = 0.25f;
        colorKeys[2].time = 0.721f;
        colorKeys[3].time = 0.774f;
        colorKeys[4].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    private Gradient generateEarthLikeLandGradient()
    {
        int numberOfKeys = 8;
        GradientColorKey[] colorKeys = new GradientColorKey[numberOfKeys];
        ColorUtility.TryParseHtmlString("#3A719F", out colorKeys[0].color);
        ColorUtility.TryParseHtmlString("#F5FF85", out colorKeys[1].color);
        ColorUtility.TryParseHtmlString("#8AD42B", out colorKeys[2].color);
        ColorUtility.TryParseHtmlString("#806D00", out colorKeys[3].color);
        ColorUtility.TryParseHtmlString("#A1530D", out colorKeys[4].color);
        ColorUtility.TryParseHtmlString("#733400", out colorKeys[5].color);
        ColorUtility.TryParseHtmlString("#FFFFFF", out colorKeys[6].color);
        ColorUtility.TryParseHtmlString("#FFFFFF", out colorKeys[7].color);
        colorKeys[0].time = 0f;
        colorKeys[1].time = 0.06f;
        colorKeys[2].time = 0.256f;
        colorKeys[3].time = 0.43f;
        colorKeys[4].time = 0.60f;
        colorKeys[5].time = 0.70f;
        colorKeys[6].time = 0.77f;
        colorKeys[7].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    private Gradient generateMarsLikeOceanGradient()
    {
        int numberOfKeys = 5;
        GradientColorKey[] colorKeys = new GradientColorKey[numberOfKeys];
        ColorUtility.TryParseHtmlString("#D98C50", out colorKeys[0].color);
        ColorUtility.TryParseHtmlString("#E79253", out colorKeys[1].color);
        ColorUtility.TryParseHtmlString("#A46C48", out colorKeys[2].color);
        ColorUtility.TryParseHtmlString("#5F4D3F", out colorKeys[3].color);
        ColorUtility.TryParseHtmlString("#3E372D", out colorKeys[4].color);
        colorKeys[0].time = 0f;
        colorKeys[1].time = 0.25f;
        colorKeys[2].time = 0.84f;
        colorKeys[3].time = 0.95f;
        colorKeys[4].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    private Gradient generateMarsLikeLandGradient()
    {
        int numberOfKeys = 5;
        GradientColorKey[] colorKeys = new GradientColorKey[numberOfKeys];
        ColorUtility.TryParseHtmlString("#9A775C", out colorKeys[0].color);
        ColorUtility.TryParseHtmlString("#AB7A55", out colorKeys[1].color);
        ColorUtility.TryParseHtmlString("#D98C50", out colorKeys[2].color);
        ColorUtility.TryParseHtmlString("#D98C50", out colorKeys[3].color);
        ColorUtility.TryParseHtmlString("#D9B294", out colorKeys[4].color);
        colorKeys[0].time = 0f;
        colorKeys[1].time = 0.25f;
        colorKeys[2].time = 0.5f;
        colorKeys[3].time = 0.8f;
        colorKeys[4].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    public GameObject generateBiomeDemoPlanet(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = 0.06f;
        layer1SimpleNoiseSettings.numberOfLayers = 5;
        layer1SimpleNoiseSettings.baseRoughness = 0.8f;
        layer1SimpleNoiseSettings.roughness = 2.21f;
        layer1SimpleNoiseSettings.persistence = 0.5f;
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = 0.98f;

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(false, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = 1.42f;
        layer2RigidNoiseSettings.numberOfLayers = 5;
        layer2RigidNoiseSettings.baseRoughness = 1.59f;
        layer2RigidNoiseSettings.roughness = 3.3f;
        layer2RigidNoiseSettings.persistence = 0.5f;
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = 0.37f;
        layer2RigidNoiseSettings.weightMultiplier = 0.78f;

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(true, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = 0.22f;
        layer3RigidNoiseSettings.numberOfLayers = 5;
        layer3RigidNoiseSettings.baseRoughness = 4;
        layer3RigidNoiseSettings.roughness = 1;
        layer3RigidNoiseSettings.persistence = 0.4f;
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = 0f;
        layer2RigidNoiseSettings.weightMultiplier = 0.22f;

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(true, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = 400f;
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = 1f;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = 2;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = 2f;
        colorNoiseSettings.simpleNoiseSettings.roughness = 1f;
        colorNoiseSettings.simpleNoiseSettings.persistence = 0.5f;
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(1, 1);
        biomes[1] = generateRandomBiome(1, 1);
        biomes[2] = generateRandomBiome(1, 1);

        biomes[0].startHeight = 0.325f;
        biomes[1].startHeight = 0.472f;
        biomes[2].startHeight = 0.888f;

        biomes[0].tint = Color.red;
        biomes[1].tint = Color.yellow;
        biomes[2].tint = Color.blue;

        biomes[0].tintPercent = 1f;
        biomes[1].tintPercent = 1f;
        biomes[2].tintPercent = 1f;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = 0.5f;
        biomeColorSettings.noiseStenght = 0.2f;
        biomeColorSettings.blendAmount = 0.1f;
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateRandomGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();

        planet.name = "?CouldNotGeneratePlanetName?";
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = 128;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), 0f, 0f, 0f);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 400f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = 0;

        return planet;
    }

    public GameObject generateEarthLikePlanetWithRandomEarthColors(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = 0.06f;
        layer1SimpleNoiseSettings.numberOfLayers = 5;
        layer1SimpleNoiseSettings.baseRoughness = 0.8f;
        layer1SimpleNoiseSettings.roughness = 2.21f;
        layer1SimpleNoiseSettings.persistence = 0.5f;
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = 0.98f;

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(false, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = 1.42f;
        layer2RigidNoiseSettings.numberOfLayers = 5;
        layer2RigidNoiseSettings.baseRoughness = 1.59f;
        layer2RigidNoiseSettings.roughness = 3.3f;
        layer2RigidNoiseSettings.persistence = 0.5f;
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = 0.37f;
        layer2RigidNoiseSettings.weightMultiplier = 0.78f;

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(true, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = 0.22f;
        layer3RigidNoiseSettings.numberOfLayers = 5;
        layer3RigidNoiseSettings.baseRoughness = 4;
        layer3RigidNoiseSettings.roughness = 1;
        layer3RigidNoiseSettings.persistence = 0.4f;
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = 0f;
        layer2RigidNoiseSettings.weightMultiplier = 0.22f;

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(true, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = 400f;
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = 1f;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = 2;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = 2f;
        colorNoiseSettings.simpleNoiseSettings.roughness = 1f;
        colorNoiseSettings.simpleNoiseSettings.persistence = 0.5f;
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(1, 1);
        biomes[1] = generateRandomBiome(1, 1);
        biomes[2] = generateRandomBiome(1, 1);

        biomes[0].startHeight = 0.325f;
        biomes[1].startHeight = 0.472f;
        biomes[2].startHeight = 0.888f;

        biomes[0].tint = Color.white;
        biomes[1].tint = Color.white;
        biomes[2].tint = Color.white;

        biomes[0].tintPercent = 0f;
        biomes[1].tintPercent = 0f;
        biomes[2].tintPercent = 0f;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = 0.5f;
        biomeColorSettings.noiseStenght = 0.2f;
        biomeColorSettings.blendAmount = 0.1f;
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateEarthLikeOceanGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();

        planet.name = "?CouldNotGeneratePlanetName?";
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = 128;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), 0f, 0f, 0f);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 400f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = 0;

        return planet;
    }

    public GameObject generateEarthLikePlanetWithoutBiomes(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = 0.06f;
        layer1SimpleNoiseSettings.numberOfLayers = 5;
        layer1SimpleNoiseSettings.baseRoughness = 0.8f;
        layer1SimpleNoiseSettings.roughness = 2.21f;
        layer1SimpleNoiseSettings.persistence = 0.5f;
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = 0.98f;

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(false, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = 1.42f;
        layer2RigidNoiseSettings.numberOfLayers = 5;
        layer2RigidNoiseSettings.baseRoughness = 1.59f;
        layer2RigidNoiseSettings.roughness = 3.3f;
        layer2RigidNoiseSettings.persistence = 0.5f;
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = 0.37f;
        layer2RigidNoiseSettings.weightMultiplier = 0.78f;

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(true, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = 0.22f;
        layer3RigidNoiseSettings.numberOfLayers = 5;
        layer3RigidNoiseSettings.baseRoughness = 4;
        layer3RigidNoiseSettings.roughness = 1;
        layer3RigidNoiseSettings.persistence = 0.4f;
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = 0f;
        layer2RigidNoiseSettings.weightMultiplier = 0.22f;

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(true, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = 400f;
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = 1f;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = 2;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = 2f;
        colorNoiseSettings.simpleNoiseSettings.roughness = 1f;
        colorNoiseSettings.simpleNoiseSettings.persistence = 0.5f;
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(1, 1);
        biomes[1] = generateRandomBiome(1, 1);
        biomes[2] = generateRandomBiome(1, 1);

        biomes[0].gradient = generateEarthLikeLandGradient();
        biomes[1].gradient = generateEarthLikeLandGradient();
        biomes[2].gradient = generateEarthLikeLandGradient();

        biomes[0].startHeight = 0.325f;
        biomes[1].startHeight = 0.472f;
        biomes[2].startHeight = 0.888f;

        biomes[0].tint = Color.white;
        biomes[1].tint = Color.white;
        biomes[2].tint = Color.white;

        biomes[0].tintPercent = 0f;
        biomes[1].tintPercent = 0f;
        biomes[2].tintPercent = 0f;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = 0.5f;
        biomeColorSettings.noiseStenght = 0.2f;
        biomeColorSettings.blendAmount = 0.1f;
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateEarthLikeOceanGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();

        planet.name = "?CouldNotGeneratePlanetName?";
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = 128;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), 0f, 0f, 0f);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 400f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = 0;

        return planet;
    }

    public GameObject generateEarthLikePlanet(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = 0.06f;
        layer1SimpleNoiseSettings.numberOfLayers = 5;
        layer1SimpleNoiseSettings.baseRoughness = 0.8f;
        layer1SimpleNoiseSettings.roughness = 2.21f;
        layer1SimpleNoiseSettings.persistence = 0.5f;
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = 0.98f;

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(false, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = 1.42f;
        layer2RigidNoiseSettings.numberOfLayers = 5;
        layer2RigidNoiseSettings.baseRoughness = 1.59f;
        layer2RigidNoiseSettings.roughness = 3.3f;
        layer2RigidNoiseSettings.persistence = 0.5f;
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = 0.37f;
        layer2RigidNoiseSettings.weightMultiplier = 0.78f;

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(true, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = 0.22f;
        layer3RigidNoiseSettings.numberOfLayers = 5;
        layer3RigidNoiseSettings.baseRoughness = 4;
        layer3RigidNoiseSettings.roughness = 1;
        layer3RigidNoiseSettings.persistence = 0.4f;
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = 0f;
        layer2RigidNoiseSettings.weightMultiplier = 0.22f;

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(true, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = 400f;
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = 1f;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = 2;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = 2f;
        colorNoiseSettings.simpleNoiseSettings.roughness = 1f;
        colorNoiseSettings.simpleNoiseSettings.persistence = 0.5f;
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(1, 1);
        biomes[1] = generateRandomBiome(1, 1);
        biomes[2] = generateRandomBiome(1, 1);

        biomes[0].gradient = generateEarthLikeLandGradient();
        biomes[1].gradient = generateEarthLikeLandGradient();
        biomes[2].gradient = generateEarthLikeLandGradient();

        biomes[0].startHeight = 0.325f;
        biomes[1].startHeight = 0.1f;
        biomes[2].startHeight = 0.9f;

        biomes[0].tint = Color.white;
        biomes[1].tint = Color.white;
        biomes[2].tint = Color.white;

        biomes[0].tintPercent = 0.4f;
        biomes[1].tintPercent = 0f;
        biomes[2].tintPercent = 0.4f;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = 0.5f;
        biomeColorSettings.noiseStenght = 0.2f;
        biomeColorSettings.blendAmount = 0.1f;
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateEarthLikeOceanGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();

        planet.name = "?CouldNotGeneratePlanetName?";
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = 128;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), 0f, 0f, 0f);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 400f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = 0;

        return planet;
    }

    public GameObject generateMarsLikePlanet(Transform targetTransform, float orbitDistance)
    {
        #region Shape Noise Layers Random Value Assignments
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        layer1NoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = 0.04f;
        layer1SimpleNoiseSettings.numberOfLayers = 5;
        layer1SimpleNoiseSettings.baseRoughness = 0.8f;
        layer1SimpleNoiseSettings.roughness = 2.21f;
        layer1SimpleNoiseSettings.persistence = 0.5f;
        layer1SimpleNoiseSettings.centre.x = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.y = generateRandomFloat();
        layer1SimpleNoiseSettings.centre.z = generateRandomFloat();
        layer1SimpleNoiseSettings.minValue = 0.98f;

        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(false, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        layer2NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = 1.42f;
        layer2RigidNoiseSettings.numberOfLayers = 5;
        layer2RigidNoiseSettings.baseRoughness = 1.59f;
        layer2RigidNoiseSettings.roughness = 3.3f;
        layer2RigidNoiseSettings.persistence = 0.5f;
        layer2RigidNoiseSettings.centre.x = generateRandomFloat();
        layer2RigidNoiseSettings.centre.y = generateRandomFloat();
        layer2RigidNoiseSettings.centre.z = generateRandomFloat();
        layer2RigidNoiseSettings.minValue = 0.37f;
        layer2RigidNoiseSettings.weightMultiplier = 0.78f;

        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(true, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        layer3NoiseSettings.filterType = NoiseSettings.FilterType.Rigid;
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = 0.22f;
        layer3RigidNoiseSettings.numberOfLayers = 5;
        layer3RigidNoiseSettings.baseRoughness = 4;
        layer3RigidNoiseSettings.roughness = 1;
        layer3RigidNoiseSettings.persistence = 0.4f;
        layer3RigidNoiseSettings.centre.x = generateRandomFloat();
        layer3RigidNoiseSettings.centre.y = generateRandomFloat();
        layer3RigidNoiseSettings.centre.z = generateRandomFloat();
        layer3RigidNoiseSettings.minValue = 0f;
        layer2RigidNoiseSettings.weightMultiplier = 0.22f;

        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(true, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        #endregion

        // assign the randomized noise layers
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = 400f;
        planetShapeSettings.noiseLayers = shapeNoiseLayers;

        #region Color Noise Random Value Assignments
        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = 1f;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = 2;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = 2f;
        colorNoiseSettings.simpleNoiseSettings.roughness = 1f;
        colorNoiseSettings.simpleNoiseSettings.persistence = 0.5f;
        colorNoiseSettings.simpleNoiseSettings.centre.x = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.y = generateRandomFloat();
        colorNoiseSettings.simpleNoiseSettings.centre.z = generateRandomFloat();

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = generateRandomBiome(1, 1);
        biomes[1] = generateRandomBiome(1, 1);
        biomes[2] = generateRandomBiome(1, 1);

        biomes[0].gradient = generateMarsLikeLandGradient();
        biomes[1].gradient = generateMarsLikeLandGradient();
        biomes[2].gradient = generateMarsLikeLandGradient();

        biomes[0].startHeight = 0.325f;
        biomes[1].startHeight = 0.1f;
        biomes[2].startHeight = 0.9f;

        biomes[0].tint = Color.white;
        biomes[1].tint = Color.white;
        biomes[2].tint = Color.white;

        biomes[0].tintPercent = 0f;
        biomes[1].tintPercent = 0f;
        biomes[2].tintPercent = 0f;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = 0.5f;
        biomeColorSettings.noiseStenght = 0.2f;
        biomeColorSettings.blendAmount = 0.1f;
        #endregion

        // assign the randomized color noise
        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = generateMarsLikeOceanGradient();

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planet = new GameObject();

        planet.name = "?CouldNotGeneratePlanetName?";
        planet.tag = "Planet";

        // set the planet's parent to the star system
        planet.transform.parent = targetTransform;

        PlanetGenerationSettings planetGenerationSettings = planet.AddComponent<PlanetGenerationSettings>();
        planetGenerationSettings.resolution = 128;
        planetGenerationSettings.shapeSettings = planetShapeSettings;
        planetGenerationSettings.colorSettings = planetColorSettings;

        // adding trail renderer object so it can  be accessed by the Trail 
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<Trail>();

        Orbit planetOrbit = planet.AddComponent<Orbit>();
        planetOrbit.setOrbitParameters(targetTransform.GetChild(0), 0f, 0f, 0f);
        // the planet should be inactive by default because it shouldnt be visible when we first start the game, only when we zoom in on a star
        planet.SetActive(false);

        // setting up click detection for the planet
        MouseClickDetection mouseClickDetectionScript = planet.AddComponent<MouseClickDetection>();
        SphereCollider planetCollider = planet.AddComponent<SphereCollider>();
        planetCollider.center = Vector3.zero;
        planetCollider.radius = 400f;

        // assigning the planet script to the planet
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript.index = 0;

        return planet;
    }
}
