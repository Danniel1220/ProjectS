using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    float minRadius = 1.0f;
    float maxRadius = 4.0f;

    int resolution = 40;

    public void generatePlanet(GameObject targetStarSystem, PlanetShapeSettings shapeSettings, PlanetColorSettings colorSettings)
    {
        GameObject planet = new GameObject();
        planet.name = "Planet";
        Planet planetScript = planet.AddComponent<Planet>();
        planetScript = new Planet(shapeSettings, colorSettings);

        // set the planet's parent to the star system
        planet.transform.parent= targetStarSystem.transform;

    }

    private Planet generateRandomPlanetShapeSettings()
    {
        float planetRadius = Random.Range(minRadius, maxRadius);
        PlanetShapeSettings.NoiseLayer[] noiseLayers = new PlanetShapeSettings.NoiseLayer[1];
        noiseLayers[0] = new PlanetShapeSettings.NoiseLayer(
            false, // first layer as mask
            new NoiseSettings(
                new NoiseSettings.SimpleNoiseSettings(
                    0.05f,
                    5,
                    1,
                    2.42f,
                    0.5f,
                    new Vector3(0, 0, 0),
                    1f
                    )));
        PlanetShapeSettings planetShapeSettings = new PlanetShapeSettings(planetRadius, noiseLayers);


        PlanetColorSettings.BiomeColorSettings.Biome[] biomes = new PlanetColorSettings.BiomeColorSettings.Biome[3];

        PlanetColorSettings planetColorSettings = new PlanetColorSettings(
            new PlanetColorSettings.BiomeColorSettings(
                
                ),
            new Gradient()
            );

        return new Planet(planetShapeSettings, planetColorSettings);
    }
}
