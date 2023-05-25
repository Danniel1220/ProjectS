using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlanetShapeSettings;

public static class PlanetFactory
{
    private const int DEFAULT_RESOLUTION = 40;
    private const int DEFAULT_NUMBER_OF_LAYERS = 5;

    private const float MIN_PLANET_RADIUS = 1f;
    private const float MAX_PLANET_RADIUS = 4f;

    private const float MIN_BASE_PLANET_STRENGHT = 0.02f;
    private const float MAX_BASE_PLANET_STRENGHT = 0.06f;

    private const float MIN_BASE_ROUGHNESS = 1f;
    private const float MAX_BASE_ROUGHNESS = 2f;


    // shape layer 1 noise settings
    private const bool LAYER1_USE_FIRST_LAYER_AS_MASK = false;
    private const NoiseSettings.FilterType LAYER1_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Simple;
    private const float LAYER1_SHAPE_STRENGHT = 0.04f;
    private const float LAYER1_SHAPE_BASE_ROUGHNESS = 1.38f;
    private const float LAYER1_SHAPE_ROUGHNESS = 3f;
    private const float LAYER1_SHAPE_PERSISTENCE = 0.24f;
    private const float LAYER1_SHAPE_CENTRE_X = 0f;
    private const float LAYER1_SHAPE_CENTRE_Y = 0f;
    private const float LAYER1_SHAPE_CENTRE_Z = 0f;
    private const float LAYER1_SHAPE_MIN_VALUE = 0.679f;
    
    // shape layer 2 noise settings
    private const bool LAYER2_USE_FIRST_LAYER_AS_MASK = true;
    private const NoiseSettings.FilterType LAYER2_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Rigid;
    private const float LAYER2_SHAPE_STRENGHT = 1.56f;
    private const float LAYER2_SHAPE_BASE_ROUGHNESS = 0.25f;
    private const float LAYER2_SHAPE_ROUGHNESS = 2.24f;
    private const float LAYER2_SHAPE_PERSISTENCE = 1.51f;
    private const float LAYER2_SHAPE_CENTRE_X = 0f;
    private const float LAYER2_SHAPE_CENTRE_Y = 0f;
    private const float LAYER2_SHAPE_CENTRE_Z = 0f;
    private const float LAYER2_SHAPE_MIN_VALUE = 0f;
    private const float LAYER2_SHAPE_WEIGHT_MULTIPLIER = 0f;

    // shape layer 3 noise settings
    private const bool LAYER3_USE_FIRST_LAYER_AS_MASK = true;
    private const NoiseSettings.FilterType LAYER3_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Rigid;
    private const float LAYER3_SHAPE_STRENGHT = 0.53f;
    private const float LAYER3_SHAPE_BASE_ROUGHNESS = 0f;
    private const float LAYER3_SHAPE_ROUGHNESS = 0f;
    private const float LAYER3_SHAPE_PERSISTENCE = 0f;
    private const float LAYER3_SHAPE_CENTRE_X = 0f;
    private const float LAYER3_SHAPE_CENTRE_Y = 0f;
    private const float LAYER3_SHAPE_CENTRE_Z = 0f;
    private const float LAYER3_SHAPE_MIN_VALUE = 0f;
    private const float LAYER3_SHAPE_WEIGHT_MULTIPLIER = 0f;


    // color noise settings
    private const NoiseSettings.FilterType COLOR_NOISE_FILTER_TYPE = NoiseSettings.FilterType.Simple;
    private const float COLOR_NOISE_STRENGHT = 1f;
    private const int COLOR_NOISE_NUMBER_OF_LAYERS = 1;
    private const float COLOR_NOISE_BASE_ROUGHNESS = 1f;
    private const float COLOR_NOISE_ROUGHNESS = 2f;
    private const float COLOR_NOISE_PERSISTENCE = 0.5f;
    private const float COLOR_NOISE_CENTRE_X = 0f;
    private const float COLOR_NOISE_CENTRE_Y = 0f;
    private const float COLOR_NOISE_CENTRE_Z = 0f;
    private const float COLOR_NOISE_MIN_VALUE = 0f;

    // biome 1 settings
    private const float BIOME1_START_HEIGHT = 0f;
    private const float BIOME1_TINT_PERCENT = 0.15f;

    // biome 2 settings
    private const float BIOME2_START_HEIGHT = 0.115f;
    private const float BIOME2_TINT_PERCENT = 0.0f;

    // biome 3 settings
    private const float BIOME3_START_HEIGHT = 0.95f;
    private const float BIOME3_TINT_PERCENT = 0.15f;

    // biome color settings
    private const float BIOME_COLOR_NOISE_OFFSET = 1f;
    private const float BIOME_COLOR_NOISE_STRENGHT = 1f;
    private const float BIOME_COLOR_BLEND_AMOUNT = 0.15f;




    public static void generatePlanet(Transform targetTransform)
    {
        // ----------- SHAPE NOISE LAYERS ------------------------------------------------------------------------
        // ----------- LAYER 1 -----------------------------------------------------------------------------------
        NoiseSettings layer1NoiseSettings = new NoiseSettings();
        NoiseSettings.SimpleNoiseSettings layer1SimpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        layer1SimpleNoiseSettings.strenght = LAYER1_SHAPE_STRENGHT;
        layer1SimpleNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer1SimpleNoiseSettings.baseRoughness = LAYER1_SHAPE_BASE_ROUGHNESS;
        layer1SimpleNoiseSettings.roughness = LAYER1_SHAPE_ROUGHNESS;
        layer1SimpleNoiseSettings.persistence = LAYER1_SHAPE_PERSISTENCE;
        layer1SimpleNoiseSettings.centre.x = LAYER1_SHAPE_CENTRE_X;
        layer1SimpleNoiseSettings.centre.y = LAYER1_SHAPE_CENTRE_Y;
        layer1SimpleNoiseSettings.centre.z = LAYER1_SHAPE_CENTRE_Z;
        layer1SimpleNoiseSettings.minValue = LAYER1_SHAPE_MIN_VALUE;

        layer1NoiseSettings.filterType = LAYER1_NOISE_FILTER_TYPE;
        layer1NoiseSettings.simpleNoiseSettings = layer1SimpleNoiseSettings;

        NoiseLayer layer1NoiseLayer = new NoiseLayer(LAYER1_USE_FIRST_LAYER_AS_MASK, layer1NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 2 -----------------------------------------------------------------------------------
        NoiseSettings layer2NoiseSettings = new NoiseSettings();
        NoiseSettings.RigidNoiseSettings layer2RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer2RigidNoiseSettings.strenght = LAYER2_SHAPE_STRENGHT;
        layer2RigidNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer2RigidNoiseSettings.baseRoughness = LAYER2_SHAPE_BASE_ROUGHNESS;
        layer2RigidNoiseSettings.roughness = LAYER2_SHAPE_ROUGHNESS;
        layer2RigidNoiseSettings.persistence = LAYER2_SHAPE_PERSISTENCE;
        layer2RigidNoiseSettings.centre.x = LAYER2_SHAPE_CENTRE_X;
        layer2RigidNoiseSettings.centre.y = LAYER2_SHAPE_CENTRE_Y;
        layer2RigidNoiseSettings.centre.z = LAYER2_SHAPE_CENTRE_Z;
        layer2RigidNoiseSettings.minValue = LAYER2_SHAPE_MIN_VALUE;
        layer2RigidNoiseSettings.weightMultiplier = LAYER2_SHAPE_WEIGHT_MULTIPLIER;

        layer2NoiseSettings.filterType = LAYER2_NOISE_FILTER_TYPE;
        layer2NoiseSettings.rigidNoiseSettings = layer2RigidNoiseSettings;

        NoiseLayer layer2NoiseLayer = new NoiseLayer(LAYER2_USE_FIRST_LAYER_AS_MASK, layer2NoiseSettings);
        // -------------------------------------------------------------------------------------------------------
        // ----------- LAYER 3 -----------------------------------------------------------------------------------
        NoiseSettings layer3NoiseSettings = new NoiseSettings();
        NoiseSettings.RigidNoiseSettings layer3RigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

        layer3RigidNoiseSettings.strenght = LAYER3_SHAPE_STRENGHT;
        layer3RigidNoiseSettings.numberOfLayers = DEFAULT_NUMBER_OF_LAYERS;
        layer3RigidNoiseSettings.baseRoughness = LAYER3_SHAPE_BASE_ROUGHNESS;
        layer3RigidNoiseSettings.roughness = LAYER3_SHAPE_ROUGHNESS;
        layer3RigidNoiseSettings.persistence = LAYER3_SHAPE_PERSISTENCE;
        layer3RigidNoiseSettings.centre.x = LAYER3_SHAPE_CENTRE_X;
        layer3RigidNoiseSettings.centre.y = LAYER3_SHAPE_CENTRE_Y;
        layer3RigidNoiseSettings.centre.z = LAYER3_SHAPE_CENTRE_Z;
        layer3RigidNoiseSettings.minValue = LAYER3_SHAPE_MIN_VALUE;
        layer3RigidNoiseSettings.weightMultiplier = LAYER3_SHAPE_WEIGHT_MULTIPLIER;

        layer3NoiseSettings.filterType = LAYER3_NOISE_FILTER_TYPE;
        layer3NoiseSettings.rigidNoiseSettings = layer3RigidNoiseSettings;

        NoiseLayer layer3NoiseLayer = new NoiseLayer(LAYER3_USE_FIRST_LAYER_AS_MASK, layer3NoiseSettings);
        // -------------------------------------------------------------------------------------------------------

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

        planetColorSettings.planetMaterial = new Material(Resources.Load("PlanetMat") as Material);

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
}
