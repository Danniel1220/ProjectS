using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { Simple, Rigid };
    public FilterType filterType;
    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strenght = 1;
        [Range(1, 8)]
        public int numberOfLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = 0.5f;
        public Vector3 centre;
        [Range(0, 2)]
        public float minValue;

        public SimpleNoiseSettings() { }
        public SimpleNoiseSettings(float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, Vector3 centre, float minValue)
        {
            this.strenght = strenght;
            this.numberOfLayers = numberOfLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.centre = centre;
            this.minValue = minValue;
        }
    }

    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = 0.8f;

        public RidgidNoiseSettings() { }

        public RidgidNoiseSettings(float strenght, int numberOfLayers, float baseRoughness, float roughness, float persistence, Vector3 centre, float minValue, float weightMultiplier)
        {
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

    public NoiseSettings(SimpleNoiseSettings simpleNoiseSettings)
    {
        this.filterType = FilterType.Simple;
        this.simpleNoiseSettings = simpleNoiseSettings;
    }

    public NoiseSettings(RidgidNoiseSettings ridgidNoiseSettings)
    {
        this.filterType = FilterType.Rigid;
        this.ridgidNoiseSettings = ridgidNoiseSettings;
    }

    public NoiseSettings() { }
}
