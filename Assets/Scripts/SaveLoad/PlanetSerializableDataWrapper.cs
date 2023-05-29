using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanetSerializableDataWrapper
{
    public string name;
    public float radius;
    public int shapeNoiseLayersCount;
    public ShapeNoise[] shapeNoiseLayers;
    public ColorNoise colorNoise;
    public int biomesCount;
    public Biome[] biomes;
    public Gradient oceanColor;

    [Serializable]
    public struct ShapeNoise
    {
        public bool enabled;
        public bool useFirstLayerAsMask;
        public NoiseSettings.FilterType filterType;
        public float strenght;
        public int numberOfLayers;
        public float baseRoughness;
        public float roughness;
        public float persistence;
        public Vector3 centre;
        public float minValue;
        public float weightMultiplier;

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
        public float noiseOffset;
        public float noiseStrenght;
        public float blendAmount;
        public NoiseSettings.FilterType filterType;
        public float strenght;
        public int numberOfLayers;
        public float baseRoughness;
        public float roughness;
        public float persistence;
        public Vector3 centre;
        public float minValue;

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
        public Gradient biomeGradient;
        public Color tint;
        public float startHeight;
        public float tintPercent;

        public Biome(Gradient gradient, Color tint, float startHeight, float tintPercent)
        {
            this.biomeGradient = gradient;
            this.tint = tint;
            this.startHeight = startHeight;
            this.tintPercent = tintPercent;
        }
    }

    public PlanetSerializableDataWrapper(string name, float radius, int shapeNoiseLayersCount, ShapeNoise[] shapeNoiseLayers, ColorNoise colorNoise, int biomesCount, Biome[] biomes, Gradient oceanColor)
    {
        this.name = name;
        this.radius = radius;
        this.shapeNoiseLayersCount = shapeNoiseLayersCount;
        this.shapeNoiseLayers = shapeNoiseLayers;
        this.colorNoise = colorNoise;
        this.biomesCount = biomesCount;
        this.biomes = biomes;
        this.oceanColor = oceanColor;
    }
}
