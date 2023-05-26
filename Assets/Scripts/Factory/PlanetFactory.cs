using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlanetShapeSettings;

public static class PlanetFactory
{
    private const int DEFAULT_RESOLUTION = 40;
    private const int DEFAULT_NUMBER_OF_LAYERS = 5;

    private const float MIN_PLANET_RADIUS = 1.5f;
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
    private const float COLOR_NOISE_STRENGHT = 1.95f;
    private const int COLOR_NOISE_NUMBER_OF_LAYERS = 2;
    private const float COLOR_NOISE_BASE_ROUGHNESS = 0f;
    private const float COLOR_NOISE_ROUGHNESS = 0f;
    private const float COLOR_NOISE_PERSISTENCE = 0.0f;
    private const float COLOR_NOISE_CENTRE_X = 0f;
    private const float COLOR_NOISE_CENTRE_Y = 0f;
    private const float COLOR_NOISE_CENTRE_Z = 0f;
    private const float COLOR_NOISE_MIN_VALUE = 0f;

    // biome 1 settings
    private const float BIOME1_START_HEIGHT = 0f;
    private const float BIOME1_TINT_PERCENT = 0.15f;

    // biome 2 settings
    private const float BIOME2_START_HEIGHT = 0f;
    private const float BIOME2_TINT_PERCENT = 0.0f;

    // biome 3 settings
    private const float BIOME3_START_HEIGHT = 0.969f;
    private const float BIOME3_TINT_PERCENT = 0.15f;

    // biome color settings
    private const float BIOME_COLOR_NOISE_OFFSET = 0f;
    private const float BIOME_COLOR_NOISE_STRENGHT = 0f;
    private const float BIOME_COLOR_BLEND_AMOUNT = 0.15f;




    public static void generatePlanet(Transform targetTransform)
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

        // assignign the defined noise layers (3 layers)
        PlanetShapeSettings.NoiseLayer[] shapeNoiseLayers = new PlanetShapeSettings.NoiseLayer[3];
        shapeNoiseLayers[0] = layer1NoiseLayer;
        shapeNoiseLayers[1] = layer2NoiseLayer;
        shapeNoiseLayers[2] = layer3NoiseLayer;

        // creating the planet shape settings
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.planetRadius = Random.Range(MIN_PLANET_RADIUS, MAX_PLANET_RADIUS);
        planetShapeSettings.noiseLayers = shapeNoiseLayers;


        // ----------- BIOME 1 -----------------------------------------------------------------------------------
        GradientColorKey[] biome1ColorKeys = new GradientColorKey[7];
        biome1ColorKeys[0].color = parseHexColor("#FFFFFF");
        biome1ColorKeys[1].color = parseHexColor("#FEFFDC");
        biome1ColorKeys[2].color = parseHexColor("#D6FFC9");
        biome1ColorKeys[3].color = parseHexColor("#6D845E");
        biome1ColorKeys[4].color = parseHexColor("#C0B580");
        biome1ColorKeys[5].color = parseHexColor("#595140");
        biome1ColorKeys[6].color = parseHexColor("#FFFFFF");

        biome1ColorKeys[0].time = 0.0f;
        biome1ColorKeys[1].time = 0.06f;
        biome1ColorKeys[2].time = 0.135f;
        biome1ColorKeys[3].time = 0.215f;
        biome1ColorKeys[4].time = 0.500f;
        biome1ColorKeys[5].time = 0.825f;
        biome1ColorKeys[6].time = 1.00f;

        GradientAlphaKey[] biome1AlphaKeys = new GradientAlphaKey[2];
        biome1AlphaKeys[0].alpha = 1.0f;
        biome1AlphaKeys[1].alpha = 1.0f;
        biome1AlphaKeys[0].time = 0.0f;
        biome1AlphaKeys[1].time = 1.0f;

        Gradient biome1Gradient = new Gradient();
        biome1Gradient.SetKeys(biome1ColorKeys, biome1AlphaKeys);

        PlanetColorSettings.BiomeColorSettings.Biome biome1 = new PlanetColorSettings.BiomeColorSettings.Biome(
            biome1Gradient, Color.white, BIOME1_START_HEIGHT, BIOME1_TINT_PERCENT);
        // -------------------------------------------------------------------------------------------------------
        // ----------- BIOME 2 -----------------------------------------------------------------------------------
        GradientColorKey[] biome2ColorKeys = new GradientColorKey[7];
        biome2ColorKeys[0].color = parseHexColor("#FCFF83");
        biome2ColorKeys[1].color = parseHexColor("#6DFF40");
        biome2ColorKeys[2].color = parseHexColor("#468420");
        biome2ColorKeys[3].color = parseHexColor("#CAA805");
        biome2ColorKeys[4].color = parseHexColor("#5B3E00");
        biome2ColorKeys[5].color = parseHexColor("#E9E5DD");
        biome2ColorKeys[6].color = parseHexColor("#FFFFFF");

        biome2ColorKeys[0].time = 0.0f;
        biome2ColorKeys[1].time = 0.097f;
        biome2ColorKeys[2].time = 0.3f;
        biome2ColorKeys[3].time = 0.52f;
        biome2ColorKeys[4].time = 0.7f;
        biome2ColorKeys[5].time = 0.75f;
        biome2ColorKeys[6].time = 1.00f;

        GradientAlphaKey[] biome2AlphaKeys = new GradientAlphaKey[2];
        biome2AlphaKeys[0].alpha = 1.0f;
        biome2AlphaKeys[1].alpha = 1.0f;
        biome2AlphaKeys[0].time = 0.0f;
        biome2AlphaKeys[1].time = 1.0f;

        Gradient biome2Gradient = new Gradient();
        biome2Gradient.SetKeys(biome2ColorKeys, biome2AlphaKeys);

        PlanetColorSettings.BiomeColorSettings.Biome biome2 = new PlanetColorSettings.BiomeColorSettings.Biome(
            biome2Gradient, Color.white, BIOME2_START_HEIGHT, BIOME2_TINT_PERCENT);
        // -------------------------------------------------------------------------------------------------------
        // ----------- BIOME 3 -----------------------------------------------------------------------------------
        GradientColorKey[] biome3ColorKeys = new GradientColorKey[7];
        biome3ColorKeys[0].color = parseHexColor("#FFFFFF");
        biome3ColorKeys[1].color = parseHexColor("#FEFFDC");
        biome3ColorKeys[2].color = parseHexColor("#D6FFC9");
        biome3ColorKeys[3].color = parseHexColor("#6D845E");
        biome3ColorKeys[4].color = parseHexColor("#C0B580");
        biome3ColorKeys[5].color = parseHexColor("#595140");
        biome3ColorKeys[6].color = parseHexColor("#FFFFFF");

        biome3ColorKeys[0].time = 0.0f;
        biome3ColorKeys[1].time = 0.06f;
        biome3ColorKeys[2].time = 0.135f;
        biome3ColorKeys[3].time = 0.215f;
        biome3ColorKeys[4].time = 0.500f;
        biome3ColorKeys[5].time = 0.825f;
        biome3ColorKeys[6].time = 1.00f;

        GradientAlphaKey[] biome3AlphaKeys = new GradientAlphaKey[2];
        biome3AlphaKeys[0].alpha = 1.0f;
        biome3AlphaKeys[1].alpha = 1.0f;
        biome3AlphaKeys[0].time = 0.0f;
        biome3AlphaKeys[1].time = 1.0f;

        Gradient biome3Gradient = new Gradient();
        biome3Gradient.SetKeys(biome3ColorKeys, biome3AlphaKeys);

        PlanetColorSettings.BiomeColorSettings.Biome biome3 = new PlanetColorSettings.BiomeColorSettings.Biome(
            biome3Gradient, Color.white, BIOME3_START_HEIGHT, BIOME3_TINT_PERCENT);
        // -------------------------------------------------------------------------------------------------------

        NoiseSettings colorNoiseSettings = new NoiseSettings();
        colorNoiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        colorNoiseSettings.filterType = NoiseSettings.FilterType.Simple;
        colorNoiseSettings.simpleNoiseSettings.strenght = COLOR_NOISE_STRENGHT;
        colorNoiseSettings.simpleNoiseSettings.numberOfLayers = COLOR_NOISE_NUMBER_OF_LAYERS;
        colorNoiseSettings.simpleNoiseSettings.baseRoughness = COLOR_NOISE_BASE_ROUGHNESS;
        colorNoiseSettings.simpleNoiseSettings.roughness = COLOR_NOISE_ROUGHNESS;
        colorNoiseSettings.simpleNoiseSettings.persistence = COLOR_NOISE_PERSISTENCE;
        colorNoiseSettings.simpleNoiseSettings.centre.x = COLOR_NOISE_CENTRE_X;
        colorNoiseSettings.simpleNoiseSettings.centre.y = COLOR_NOISE_CENTRE_X;
        colorNoiseSettings.simpleNoiseSettings.centre.z = COLOR_NOISE_CENTRE_X;

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];
        biomes[0] = biome1;
        biomes[1] = biome2;
        biomes[2] = biome3;

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings();
        biomeColorSettings.biomes = biomes;
        biomeColorSettings.noise = colorNoiseSettings;
        biomeColorSettings.noiseOffset = BIOME_COLOR_NOISE_OFFSET;
        biomeColorSettings.noiseStenght = BIOME_COLOR_NOISE_STRENGHT;
        biomeColorSettings.blendAmount = BIOME_COLOR_BLEND_AMOUNT;

        GradientColorKey[] oceanColorKeys = new GradientColorKey[5];
        oceanColorKeys[0].color = parseHexColor("#111B52");
        oceanColorKeys[1].color = parseHexColor("#13285B");
        oceanColorKeys[2].color = parseHexColor("#2E4582");
        oceanColorKeys[3].color = parseHexColor("#3A5FA4");
        oceanColorKeys[4].color = parseHexColor("#41AABE");

        oceanColorKeys[0].time = 0.0f;
        oceanColorKeys[1].time = 0.35f;
        oceanColorKeys[2].time = 0.62f;
        oceanColorKeys[3].time = 0.85f;
        oceanColorKeys[4].time = 1.00f;

        GradientAlphaKey[] oceanAlphaKeys = new GradientAlphaKey[2];
        oceanAlphaKeys[0].alpha = 1.0f;
        oceanAlphaKeys[1].alpha = 1.0f;
        oceanAlphaKeys[0].time = 0.0f;
        oceanAlphaKeys[1].time = 1.0f;

        Gradient oceanColorGradient = new Gradient();
        oceanColorGradient.SetKeys(oceanColorKeys, oceanAlphaKeys);

        planetColorSettings.biomeColorSettings = biomeColorSettings;
        planetColorSettings.oceanColor = oceanColorGradient;

        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

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
        planetGameObject.transform.localPosition = Vector3.zero;
    }

    private static Color parseHexColor(string hexColorCode)
    {
        Color parsedColor;

        if (ColorUtility.TryParseHtmlString(hexColorCode, out parsedColor)) return parsedColor;
        else ColorUtility.TryParseHtmlString("#FF009A", out parsedColor);
        
        return parsedColor;
    }

    private static float generateRandomFloat()
    {
        return Random.Range(-12000, 12000);
    }
    
    private static PlanetColorSettings.BiomeColorSettings.Biome generateRandomBiome(float biomeStartHeight, float biomeTintPercent)
    {
        int numberOfKeys = Random.Range(MIN_BIOME_NUMBER_OF_KEYS, MAX_BIOME_NUMBER_OF_KEYS);
        float distanceBetweenKeys = 1 / numberOfKeys;
        GradientColorKey[] biomeColorKeys = new GradientColorKey[numberOfKeys];
        for (int i = 0; i < numberOfKeys; i++)
        {
            biomeColorKeys[i].color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
            biomeColorKeys[i].time = distanceBetweenKeys * i;
        }

        GradientAlphaKey[] biomeAlphaKeys = new GradientAlphaKey[2];
        biomeAlphaKeys[0].alpha = 1.0f;
        biomeAlphaKeys[1].alpha = 1.0f;
        biomeAlphaKeys[0].time = 0.0f;
        biomeAlphaKeys[1].time = 1.0f;

        Gradient biomeGradient = new Gradient();
        biomeGradient.SetKeys(biomeColorKeys, biomeAlphaKeys);

        Color tintColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);

        return new PlanetColorSettings.BiomeColorSettings.Biome(biomeGradient, tintColor, biomeStartHeight, biomeTintPercent);
    }
}
