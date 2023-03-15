using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColorGenerator
{
    PlanetColorSettings settings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void updateSettings(PlanetColorSettings settings)
    {
        this.settings = settings;
        if (texture == null || texture.height != settings.biomeColorSettings.biomes.Length)
        {
            texture = new Texture2D(textureResolution, settings.biomeColorSettings.biomes.Length, TextureFormat.RGBA32, false);
        }
        biomeNoiseFilter = NoiseFilterFactory.createNoiseFilter(settings.biomeColorSettings.noise);
    }

    public void updateElevation(PlanetMinMaxHeight elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.min, elevationMinMax.max, 0, 0));
    }

    public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        float heightPercent = (pointOnUnitSphere.y + 1) / 2f;
        heightPercent += (biomeNoiseFilter.evaluateNoise(pointOnUnitSphere) - settings.biomeColorSettings.noiseOffset) * settings.biomeColorSettings.noiseStenght;
        float biomeIndex = 0;
        int numBiomes = settings.biomeColorSettings.biomes.Length;
        float blendRange = settings.biomeColorSettings.blendAmount / 2 + 0.001f;

        for (int i = 0; i < numBiomes; i++)
        {
            float distance = heightPercent - settings.biomeColorSettings.biomes[i].startHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, distance);
            biomeIndex *= (1 - weight);
            biomeIndex += i * weight;
        }

        return biomeIndex / Mathf.Max(1, numBiomes - 1);
    }

    public void updateColors()
    {
        Color[] colors = new Color[texture.width * texture.height];

        int colorIndex = 0;
        foreach (PlanetColorSettings.BiomeColorSettings.Biome biome in settings.biomeColorSettings.biomes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                Color gradientColor = biome.gradient.Evaluate(i / (textureResolution - 1f));
                Color tintColor = biome.tint;
                colors[colorIndex] = gradientColor * (1 - biome.tintPercent) + tintColor * biome.tintPercent;
                colorIndex++;
            }
        }

        
        texture.SetPixels(colors);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}
