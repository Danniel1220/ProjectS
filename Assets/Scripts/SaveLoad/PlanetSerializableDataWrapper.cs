using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanetSerializableDataWrapper
{
    string name;
    float radius;
    int shapeNoiseLayersCount;
    ShapeNoise[] shapeNoiseLayers;
    ColorNoise colorNoise;
    int biomesCount;
    Biome[] biomes;

    [Serializable]
    public struct ShapeNoise
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

        public ShapeNoise(bool enabled, bool useFirstLayerAsMask, NoiseSettings.FilterType filterType, float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, Vector3 centre, float minValue, float weightMultiplier)
        {
            this.enabled = enabled;
            this.useFirstLayerAsMask = useFirstLayerAsMask;
            this.filterType = filterType;
            this.strenght = strenght;
            this.numberOfLayers = numberOfLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.centre = centre;
            this.minValue = minValue;
            this.weightMultiplier = weightMultiplier;
        }
    }

    [Serializable]
    public struct ColorNoise
    {
        float noiseOffset;
        float noiseStrenght;
        float blendAmount;
        NoiseSettings.FilterType filterType;
        float strenght;
        int numberOfLayers;
        float baseRoughness;
        float roughness;
        float persistence;
        Vector3 centre;
        float minValue;

        public ColorNoise(float noiseOffset, float noiseStrenght, float blendAmount, NoiseSettings.FilterType filterType, float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, Vector3 centre, float minValue)
        {
            this.noiseOffset = noiseOffset;
            this.noiseStrenght = noiseStrenght;
            this.blendAmount = blendAmount;
            this.filterType = filterType;
            this.strenght = strenght;
            this.numberOfLayers = numberOfLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.centre = centre;
            this.minValue = minValue;
        }
    }

    [Serializable]
    public struct Biome
    {
        Gradient gradient;
        Color tint;
        float startHeight;
        float tintPercent;

        public Biome(Gradient gradient, Color tint, float startHeight, float tintPercent)
        {
            this.gradient = gradient;
            this.tint = tint;
            this.startHeight = startHeight;
            this.tintPercent = tintPercent;
        }
    }

    public PlanetSerializableDataWrapper(string name, float radius, int shapeNoiseLayersCount, ShapeNoise[] shapeNoiseLayers, ColorNoise colorNoise, int biomesCount, Biome[] biomes)
    {
        this.name = name;
        this.radius = radius;
        this.shapeNoiseLayersCount = shapeNoiseLayersCount;
        this.shapeNoiseLayers = shapeNoiseLayers;
        this.colorNoise = colorNoise;
        this.biomesCount = biomesCount;
        this.biomes = biomes;
    }
}
