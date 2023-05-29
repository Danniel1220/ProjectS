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
        public float centreX;
        public float centreY;
        public float centreZ;
        public float minValue;
        public float weightMultiplier;

        public ShapeNoise(bool enabled, bool useFirstLayerAsMask, NoiseSettings.FilterType filterType, float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, float centreX, float centreY, float centreZ, float minValue, float weightMultiplier)
        {
            this.enabled = enabled;
            this.useFirstLayerAsMask = useFirstLayerAsMask;
            this.filterType = filterType;
            this.strenght = strenght;
            this.numberOfLayers = numberOfLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.centreX = centreX;
            this.centreY = centreY;
            this.centreZ = centreZ;
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
        public float centreX;
        public float centreY;
        public float centreZ;
        public float minValue;

        public ColorNoise(float noiseOffset, float noiseStrenght, float blendAmount, NoiseSettings.FilterType filterType, float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, float centreX, float centreY, float centreZ, float minValue)
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
            this.centreX = centreX;
            this.centreY = centreY;
            this.centreZ = centreZ;
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

            Debug.Log("Log from Biome constructor: " + gradient);
        }
    }

    [Serializable]
    public struct Gradient
    {
        public List<AlphaKey> alphaKeys;
        public List<ColorKey> colorKeys;

        public Gradient(UnityEngine.Gradient gradient)
        {
            List<AlphaKey> alphaKeysAux = new List<AlphaKey>();
            List<ColorKey> colorKeysAux = new List<ColorKey>();

            for (int i = 0; i < gradient.alphaKeys.Length; i++)
            {
                alphaKeysAux.Add(new AlphaKey(gradient.alphaKeys[i]));
            }
            for (int i = 0; i < gradient.colorKeys.Length; i++)
            {
                colorKeysAux.Add(new ColorKey(gradient.colorKeys[i]));
            }

            this.alphaKeys = alphaKeysAux;
            this.colorKeys = colorKeysAux;

            Debug.Log("Log from Gradient constructor: " + alphaKeysAux);
            Debug.Log("Log from Gradient constructor: " + colorKeysAux);
        }
    }

    [Serializable]
    public struct AlphaKey
    {
        public float alpha;
        public float time;

        public AlphaKey(UnityEngine.GradientAlphaKey gradientAlphaKey)
        {
            this.alpha = gradientAlphaKey.alpha;
            this.time = gradientAlphaKey.time;
        }
    }

    [Serializable]
    public struct ColorKey
    {
        public Color color;
        public float time;

        public ColorKey(UnityEngine.GradientColorKey gradientColorKey)
        {
            this.color = new Color(gradientColorKey.color);
            this.time = gradientColorKey.time;
        }
    }

    [Serializable]
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(UnityEngine.Color color)
        {
            this.r = color.r;
            this.g = color.g;
            this.b = color.b;
            this.a = color.a;
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
