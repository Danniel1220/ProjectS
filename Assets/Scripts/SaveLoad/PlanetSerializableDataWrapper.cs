using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanetSerializableDataWrapper
{
    float planetRadius;
    int shapeNoiseLayersCount;
    ShapeNoise[] shapeNoiseLayers;
    ColorNoise colorNoise;
    int biomesCount;
    Biome[] biomes;

    [Serializable]
    struct ShapeNoise
    {
        bool enabled;
        bool useFirstLayerAsMask;
        NoiseSettings.FilterType filterType;
        float strenght;
        int numberOfLayers;
        float baseRoughness;
        float roughness;
        float persistence;
        Vector3 centre;
        float minValue;
        float weightMultiplier;
    }

    [Serializable]
    struct ColorNoise
    {
        float noiseOffset;
        float noiseStrenght;
        NoiseSettings.FilterType filterType;
        float strenght;
        int numberOfLayers;
        float baseRoughness;
        float roughness;
        float persistence;
        Vector3 centre;
        float minValue;
    }

    [Serializable]
    struct Biome
    {
        Gradient gradient;
        Color tint;
        float startHeight;
        float tintPercent;
    }
}
