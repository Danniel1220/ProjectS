using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDataLoader : MonoBehaviour
{
    private StarFactory starFactory;
    private PlanetFactory planetFactory;
    private StarshipPosition starshipPosition;

    void Start()
    {
        starFactory = GameManagers.starFactory;
        planetFactory = GameManagers.planetFactory;
        starshipPosition = GameManagers.starshipPosition;
    }

    public void loadGameData()
    {
        string json = File.ReadAllText(Application.dataPath + "/SavedGameData.json");
        GameDataSerializableWrapper gameData = JsonConvert.DeserializeObject<GameDataSerializableWrapper>(json);

        // for each star system deserialized
        foreach (StarSystemSerializableDataWrapper starSystemWrapper in gameData.starSystems)
        {
            Vector3 starSystemLocation = new Vector3(starSystemWrapper.transformX, starSystemWrapper.transformY, starSystemWrapper.transformZ);

            GameObject starSystemGameObject = starFactory.createStarSystem(starSystemLocation, StarClassParser.stringToStarClass(starSystemWrapper.starClass), starSystemWrapper.name, starSystemWrapper.isHomeworld, starSystemWrapper.index);

            // for each planet deserialized in the current star system we iterate through
            foreach (PlanetSerializableDataWrapper planetWrapper in starSystemWrapper.planets)
            {
                // grabbing all the relevant data to form the full planet settings
                PlanetShapeSettings planetShapeSettings = extractPlanetShapeSettings(planetWrapper);
                PlanetColorSettings planetColorSettings = extractPlanetColorSettings(planetWrapper);

                // generate the new planet under the current iteration star system with the specified settings
                planetFactory.generatePlanet(starSystemGameObject.transform, planetShapeSettings, planetColorSettings, planetWrapper.name, planetWrapper.orbitDistance);
            }
        }

        // setting the starship position to the position it had when saving
        starshipPosition.setTargetViaStarSystemIndex(gameData.starshipTargetIndex);
    }

    private PlanetColorSettings extractPlanetColorSettings(PlanetSerializableDataWrapper planetWrapper)
    {
        PlanetColorSettings.BiomeColorSettings biomeColorSettings = extractBiomesAndBiomeSettings(planetWrapper);
        Gradient oceanColor = extractGradient(planetWrapper.oceanColor);

        PlanetColorSettings planetColorSettings = ScriptableObject.CreateInstance<PlanetColorSettings>();
        planetColorSettings.init(biomeColorSettings, oceanColor);

        // creating a new material for the planet and assigning it
        planetColorSettings.planetMaterial = new Material(Resources.Load("Planet Material") as Material);

        return planetColorSettings;
    }

    private PlanetColorSettings.BiomeColorSettings extractBiomesAndBiomeSettings(PlanetSerializableDataWrapper planetWrapper)
    {
        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[planetWrapper.biomesCount];
        for (int i = 0; i < biomes.Length; i++)
        {
            biomes[i] = new PlanetColorSettings.BiomeColorSettings.Biome(
                extractGradient(planetWrapper.biomes[i].biomeGradient),
                extractColor(planetWrapper.biomes[i].tint),
                planetWrapper.biomes[i].startHeight,
                planetWrapper.biomes[i].tintPercent);

        }

        NoiseSettings biomeColorNoiseSettings = new NoiseSettings(
            new NoiseSettings.SimpleNoiseSettings(
                planetWrapper.colorNoise.strenght,
                planetWrapper.colorNoise.numberOfLayers,
                planetWrapper.colorNoise.baseRoughness,
                planetWrapper.colorNoise.roughness,
                planetWrapper.colorNoise.persistence,
                new Vector3(planetWrapper.colorNoise.centreX, planetWrapper.colorNoise.centreY, planetWrapper.colorNoise.centreX),
                planetWrapper.colorNoise.minValue));

        PlanetColorSettings.BiomeColorSettings biomeColorSettings = new PlanetColorSettings.BiomeColorSettings(
            biomes,
            biomeColorNoiseSettings,
            planetWrapper.colorNoise.noiseOffset,
            planetWrapper.colorNoise.noiseStrenght,
            planetWrapper.colorNoise.blendAmount);
        return biomeColorSettings;
    }

    private PlanetShapeSettings extractPlanetShapeSettings(PlanetSerializableDataWrapper planetWrapper)
    {
        PlanetShapeSettings.NoiseLayer[] noiseLayers = new PlanetShapeSettings.NoiseLayer[planetWrapper.shapeNoiseLayersCount];
        for (int i = 0; i < noiseLayers.Length; i++)
        {
            NoiseSettings noiseSettings = new NoiseSettings();
            if (planetWrapper.shapeNoiseLayers[i].filterType == NoiseSettings.FilterType.Simple)
            {
                noiseSettings = extractSimpleNoiseSettings(planetWrapper, i);
            }
            else if (planetWrapper.shapeNoiseLayers[i].filterType == NoiseSettings.FilterType.Rigid)
            {
                noiseSettings = extractRigidNoiseSettings(planetWrapper, i);
            }

            // if the noise settings are still equal to a new empty NoiseSettings class that means the filter type we checked for
            // did not exist, which should never happen
            if (noiseSettings.filterType.Equals(new NoiseSettings())) Debug.LogError("Deserializing filter type error...");

            noiseLayers[i] = new PlanetShapeSettings.NoiseLayer(planetWrapper.shapeNoiseLayers[i].useFirstLayerAsMask, noiseSettings);
        }
        PlanetShapeSettings planetShapeSettings = ScriptableObject.CreateInstance<PlanetShapeSettings>();
        planetShapeSettings.init(planetWrapper.radius, noiseLayers);

        return planetShapeSettings;
    }

    private NoiseSettings extractRigidNoiseSettings(PlanetSerializableDataWrapper planetWrapper, int i)
    {
        NoiseSettings noiseSettings;
        NoiseSettings.RigidNoiseSettings rigidNoiseSettings = new NoiseSettings.RigidNoiseSettings(
                                    planetWrapper.shapeNoiseLayers[i].strenght,
                                    planetWrapper.shapeNoiseLayers[i].numberOfLayers,
                                    planetWrapper.shapeNoiseLayers[i].baseRoughness,
                                    planetWrapper.shapeNoiseLayers[i].roughness,
                                    planetWrapper.shapeNoiseLayers[i].persistence,
                                    new Vector3(planetWrapper.shapeNoiseLayers[i].centreX, planetWrapper.shapeNoiseLayers[i].centreY, planetWrapper.shapeNoiseLayers[i].centreZ),
                                    planetWrapper.shapeNoiseLayers[i].minValue,
                                    planetWrapper.shapeNoiseLayers[i].weightMultiplier
                                    );
        noiseSettings = new NoiseSettings(rigidNoiseSettings);
        return noiseSettings;
    }

    private NoiseSettings extractSimpleNoiseSettings(PlanetSerializableDataWrapper planetWrapper, int i)
    {
        NoiseSettings noiseSettings;
        NoiseSettings.SimpleNoiseSettings simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings(
                                    planetWrapper.shapeNoiseLayers[i].strenght,
                                    planetWrapper.shapeNoiseLayers[i].numberOfLayers,
                                    planetWrapper.shapeNoiseLayers[i].baseRoughness,
                                    planetWrapper.shapeNoiseLayers[i].roughness,
                                    planetWrapper.shapeNoiseLayers[i].persistence,
                                    new Vector3(planetWrapper.shapeNoiseLayers[i].centreX, planetWrapper.shapeNoiseLayers[i].centreY, planetWrapper.shapeNoiseLayers[i].centreZ),
                                    planetWrapper.shapeNoiseLayers[i].minValue
                                    );
        noiseSettings = new NoiseSettings(simpleNoiseSettings);
        return noiseSettings;
    }

    private Gradient extractGradient(PlanetSerializableDataWrapper.Gradient wrappedGradient)
    {
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[wrappedGradient.alphaKeys.Length];
        GradientColorKey[] colorKeys = new GradientColorKey[wrappedGradient.colorKeys.Length];

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i] = new GradientAlphaKey(wrappedGradient.alphaKeys[i].alpha, wrappedGradient.alphaKeys[i].time);
        }

        for (int i = 0; i < colorKeys.Length; i++)
        {
            Color color = extractColor(wrappedGradient.colorKeys[i].color);

            colorKeys[i] = new GradientColorKey(color, wrappedGradient.colorKeys[i].time);
        }

        Gradient gradient = new Gradient();
        gradient.alphaKeys = alphaKeys;
        gradient.colorKeys = colorKeys;

        return gradient;
    }

    private Color extractColor(PlanetSerializableDataWrapper.Color color)
    {
        return new Color(color.r, color.g, color.b, color.a);
    }
}
