using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetColorSettings : ScriptableObject
{
    public Material planetMaterial;
    public BiomeColorSettings biomeColorSettings;
    public Gradient oceanColor;
    public PlanetColorSettings() { }

    public PlanetColorSettings(BiomeColorSettings biomeColorSettings, Gradient oceanColor)
    {
        this.biomeColorSettings = biomeColorSettings;
        this.oceanColor = oceanColor;
    }

    public void init(BiomeColorSettings biomeColorSettings, Gradient oceanColor)
    {
        this.biomeColorSettings = biomeColorSettings;
        this.oceanColor = oceanColor;
    }

    public void init(PlanetColorSettings planetColorSettings)
    {
        this.biomeColorSettings = planetColorSettings.biomeColorSettings;
        this.oceanColor = planetColorSettings.oceanColor;
    }

    [System.Serializable]
    public class BiomeColorSettings
    {
        public Biome[] biomes;
        public NoiseSettings noise;
        public float noiseOffset;
        public float noiseStenght;
        [Range(0, 1)]
        public float blendAmount;

        [System.Serializable]
        public class Biome
        {
            public Gradient gradient;
            public Color tint;
            [Range(0, 1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;

            public Biome() { }

            public Biome(Gradient gradient, Color tint, float startHeight, float tintPercent)
            {
                this.gradient = gradient;
                this.tint = tint;
                this.startHeight = startHeight;
                this.tintPercent = tintPercent;
            }
        }

        public BiomeColorSettings() { }

        public BiomeColorSettings(Biome[] biomes, NoiseSettings noise, float noiseOffset, float noiseStenght, float blendAmount)
        {
            this.biomes = biomes;
            this.noise = noise;
            this.noiseOffset = noiseOffset;
            this.noiseStenght = noiseStenght;
            this.blendAmount = blendAmount;
        }
    }
}
