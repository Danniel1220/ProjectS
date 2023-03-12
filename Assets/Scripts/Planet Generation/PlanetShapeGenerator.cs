using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShapeGenerator
{
    PlanetShapeSettings settings;
    INoiseFilter[] noiseFilters;
    public PlanetMinMaxHeight elevationMinMax;

    public void updateSettings(PlanetShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax= new PlanetMinMaxHeight();
    }

    public Vector3 calculatePointOnPlanet (Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].evaluateNoise(pointOnUnitSphere);
            if (settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue; 
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].evaluateNoise(pointOnUnitSphere) * mask;
            }
        }
        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.addValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
