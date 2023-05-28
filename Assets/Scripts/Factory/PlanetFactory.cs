using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlanetShapeSettings;

public static class PlanetFactory
{
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

    public static GameObject generatePlanet(Transform targetTransform)
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

        // creating a new material for the planet
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        // creating the planet game object
        GameObject planetGameObject = new GameObject();
        planetGameObject.AddComponent<Planet>();
        Planet planetScript = planetGameObject.GetComponent<Planet>();
        planetScript.shapeSettings = planetShapeSettings;
        planetScript.colorSettings = planetColorSettings;
        planetScript.resolution = DEFAULT_RESOLUTION;
        planetGameObject.name = "Auto Generated Planet";
        planetGameObject.tag = "Planet";

        // set the planet's parent to the star system
        planetGameObject.transform.parent = targetTransform;

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

        lightGameObjectNorth.transform.parent = planetGameObject.transform;
        lightGameObjectSouth.transform.parent = planetGameObject.transform;
        lightGameObjectEast.transform.parent = planetGameObject.transform;
        lightGameObjectWest.transform.parent = planetGameObject.transform;

        lightGameObjectNorth.transform.localPosition = new Vector3(0, 0, PLANET_LIGHT_OFFSET);
        lightGameObjectSouth.transform.localPosition = new Vector3(0, 0, -PLANET_LIGHT_OFFSET);
        lightGameObjectEast.transform.localPosition = new Vector3(PLANET_LIGHT_OFFSET, 0, 0);
        lightGameObjectWest.transform.localPosition = new Vector3(-PLANET_LIGHT_OFFSET, 0, 0);

        // adding trail renderer object so it can later be accessed by the Trail class and tweaked in StarFactory
        planetGameObject.AddComponent<TrailRenderer>();

        return planetGameObject;
    }

    private static float generateRandomFloat()
    {
        return Random.Range(-12000, 12000);
    }
    
    private static PlanetColorSettings.BiomeColorSettings.Biome generateRandomBiome(float biomeStartHeight, float biomeTintPercent)
    {
        Color tintColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);

        return new PlanetColorSettings.BiomeColorSettings.Biome(generateRandomGradient(), tintColor, biomeStartHeight, biomeTintPercent);
    }

    private static Gradient generateRandomGradient()
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

    public static float getMaxPlanetRadius()
    {
        return MAX_PLANET_RADIUS;
    }
}
