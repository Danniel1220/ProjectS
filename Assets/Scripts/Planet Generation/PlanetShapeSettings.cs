using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask; 
        public NoiseSettings noiseSettings;

        public NoiseLayer(bool useFirstLayerAsMask, NoiseSettings noiseSettings)
        {
            this.useFirstLayerAsMask = useFirstLayerAsMask;
            this.noiseSettings = noiseSettings;
        }
    }

    public PlanetShapeSettings(float planetRadius, NoiseLayer[] noiseLayers)
    {
        this.planetRadius = planetRadius;
        this.noiseLayers = noiseLayers;
    }
}
