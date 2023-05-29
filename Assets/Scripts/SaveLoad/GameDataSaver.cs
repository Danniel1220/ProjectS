using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;
using FullSerializer;
using UnityEngine.Playables;

public static class GameDataSaver
{
    static GalaxyChunkSystem galaxyChunkSystem = GameManagers.galaxyChunkSystem;

    public static void saveGameData()
    {
        // serializable list where all star system data gets stored
        List<StarSystemSerializableDataWrapper> serializableStarSystems = new List<StarSystemSerializableDataWrapper>();

        List<GalaxyChunk> chunks =  galaxyChunkSystem.getAllChunks();

        // for each chunk in the game
        foreach (GalaxyChunk chunk in chunks)
        {
            Debug.Log("Checking chunk " + chunk.chunkPosition);
            // for each object in each chunk
            foreach (GameObject starSystem in chunk.chunkGameObjectList)
            {
                // check if the game object we are iterating through is really a star system
                if (starSystem.transform.tag == "Star System")
                {
                    // serializable list where all the planets in the star system will get stored
                    List<PlanetSerializableDataWrapper> serializablePlanets = new List<PlanetSerializableDataWrapper>();

                    // getting the name of the star system, which is the first word of the name
                    string starSystemName = starSystem.name.Split(' ')[0];

                    Debug.Log(starSystem.transform.Find(starSystemName).transform.Find("StarSphere").transform.tag);
                    // retrieving the star class of this star system
                    StarClass starClass = StarClassParser.parseGameObjectTagToStarClass(starSystem.transform.Find(starSystemName).transform.Find("StarSphere").transform.tag);

                    Debug.Log("Serializing star system " + starSystemName);

                    // iterating through all children of the star system game object
                    foreach (Transform planet in starSystem.transform)
                    {
                        // if the object on the current iteration is actually a planet
                        if (planet.tag == "Planet")
                        {
                            Planet planetScript = planet.GetComponent<Planet>();

                            // grabbing all the noise layers that make up the planet's shape and placing them in serializable wrappers
                            PlanetSerializableDataWrapper.ShapeNoise[] shapeNoiseLayers = new PlanetSerializableDataWrapper.ShapeNoise[planetScript.shapeSettings.noiseLayers.Length];
                            for (int i = 0; i < planetScript.shapeSettings.noiseLayers.Length; i++)
                            {
                                switch (planetScript.shapeSettings.noiseLayers[i].noiseSettings.filterType)
                                {
                                    case NoiseSettings.FilterType.Simple:
                                        shapeNoiseLayers[i] = new PlanetSerializableDataWrapper.ShapeNoise(
                                    planetScript.shapeSettings.noiseLayers[i].enabled,
                                    planetScript.shapeSettings.noiseLayers[i].useFirstLayerAsMask,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.filterType,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.strenght,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.numberOfLayers,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.baseRoughness,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.roughness,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.persistence,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.centre,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.simpleNoiseSettings.minValue,
                                    -1f);
                                        break;
                                    case NoiseSettings.FilterType.Rigid:
                                        shapeNoiseLayers[i] = new PlanetSerializableDataWrapper.ShapeNoise(
                                    planetScript.shapeSettings.noiseLayers[i].enabled,
                                    planetScript.shapeSettings.noiseLayers[i].useFirstLayerAsMask,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.filterType,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.strenght,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.numberOfLayers,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.baseRoughness,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.roughness,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.persistence,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.centre,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.minValue,
                                    planetScript.shapeSettings.noiseLayers[i].noiseSettings.rigidNoiseSettings.weightMultiplier);
                                        break;
                                }
                            }

                            // grabbind the color noise and placing it in a serializable layer
                            PlanetSerializableDataWrapper.ColorNoise colorNoise = new PlanetSerializableDataWrapper.ColorNoise(
                                planetScript.colorSettings.biomeColorSettings.noiseOffset,
                                planetScript.colorSettings.biomeColorSettings.noiseStenght,
                                planetScript.colorSettings.biomeColorSettings.blendAmount,
                                planetScript.colorSettings.biomeColorSettings.noise.filterType,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.strenght,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.numberOfLayers,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.baseRoughness,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.roughness,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.persistence,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.centre,
                                planetScript.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.minValue);


                            PlanetSerializableDataWrapper.Biome[] biomes = new PlanetSerializableDataWrapper.Biome[planetScript.colorSettings.biomeColorSettings.biomes.Count()];
                            for (int i = 0; i < planetScript.colorSettings.biomeColorSettings.biomes.Count(); i++)
                            {
                                PlanetSerializableDataWrapper.Biome biome = new PlanetSerializableDataWrapper.Biome(
                                    planetScript.colorSettings.biomeColorSettings.biomes[i].gradient,
                                    planetScript.colorSettings.biomeColorSettings.biomes[i].tint,
                                    planetScript.colorSettings.biomeColorSettings.biomes[i].startHeight,
                                    planetScript.colorSettings.biomeColorSettings.biomes[i].tintPercent);
                            }

                            serializablePlanets.Add(new PlanetSerializableDataWrapper(
                                planet.name,
                                planetScript.shapeSettings.planetRadius,
                                planetScript.shapeSettings.noiseLayers.Length,
                                shapeNoiseLayers,
                                colorNoise,
                                planetScript.colorSettings.biomeColorSettings.biomes.Count(),
                                biomes,
                                planetScript.colorSettings.oceanColor));

                            
                        }
                    }
                    serializableStarSystems.Add(new StarSystemSerializableDataWrapper(
                            starSystem.transform,
                            starClass,
                            starSystemName,
                            serializablePlanets));
                }
            }
        }
        Debug.Log("Star systems serialized:" + serializableStarSystems.Count());
        fsSerializer serializer = new fsSerializer();
        fsData data;
        serializer.TrySerialize(typeof(List<StarSystemSerializableDataWrapper>), serializableStarSystems, out data);
        saveJsonToFile(Application.dataPath + "/SavedGameData.json", fsJsonPrinter.CompressedJson(data));


        /*GameDataSerializableWrapper gameData = new GameDataSerializableWrapper(serializableStarSystems);
        Debug.Log("Star systems serialized:" + serializableStarSystems.Count());
        saveJsonToFile(Application.dataPath + "/SavedGameData.json", JsonUtility.ToJson(gameData));

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.dataPath + "/SavedGameData.bin", (int)FileMode.Create);

        formatter.Serialize(saveFile, serializableStarSystems);
        saveFile.Close();*/
    }

    private static void saveJsonToFile(string path, string json)
    {
        File.WriteAllText(path, json);
    }
}
