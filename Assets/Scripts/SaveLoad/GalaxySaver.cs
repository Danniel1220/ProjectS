using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class GalaxySaver : MonoBehaviour
{
    GalaxyChunkSystem galaxyChunkSystem;
    List<GalaxyChunk> chunkList;
    public bool saveJson = false;
    public bool saveBinary = false;
    // Start is called before the first frame update
    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GameManager").GetComponent<GalaxyChunkSystem>();
        chunkList = galaxyChunkSystem.getAllChunks();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveJson)
        {
            GalaxyViewStarDataWrapper dataWrapper = new GalaxyViewStarDataWrapper(getStarData());
            Debug.Log("Stars count:" + dataWrapper.starData.Count);
            saveJsonToFile(Application.dataPath + "/StarLocations.json", JsonUtility.ToJson(dataWrapper));
            saveJson = false;
        }
        if (saveBinary)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Create(Application.dataPath + "/StarLocations.bin", (int)FileMode.Create);
            GalaxyViewStarDataWrapper dataWrapper = new GalaxyViewStarDataWrapper(getStarData());

            formatter.Serialize(saveFile, dataWrapper);
            Debug.Log("Stars count:" + dataWrapper.starData.Count);
            saveFile.Close();
            saveBinary = false;
        }
    }

    private List<GalaxyViewStarSerializableData> getStarData()
    {
        List<GalaxyViewStarSerializableData> dataList = new List<GalaxyViewStarSerializableData>();

        Debug.Log("Getting all chunks...");
        chunkList = galaxyChunkSystem.getAllChunks();
        Debug.Log("Chunk count:" + chunkList.Count);

        foreach (GalaxyChunk chunk in chunkList)
        {
            foreach (GameObject star in chunk.chunkGameObjectList)
            {
                // only grabbing stars
                if (star.tag == "Star")
                {
                    StarClass starClass = parseTagToStarClassEnum(star.transform.Find("StarSphere").tag);
                    GalaxyViewStarSerializableData starData = new GalaxyViewStarSerializableData(
                        star.transform.position.x, 
                        star.transform.position.y, 
                        star.transform.position.z, 
                        starClass.ToString());
                    dataList.Add(starData);
                }
            }
        }
        return dataList;
    }

    private void saveJsonToFile(string path, string json)
    {
        File.WriteAllText(path, json);
    }

    private StarClass parseTagToStarClassEnum (string tag)
    {
        switch (tag)
        {
            case "Class M Star":
                return StarClass.M;
            case "Class K Star":
                return StarClass.K;
            case "Class G Star":
                return StarClass.G;
            case "Class F Star":
                return StarClass.F;
            case "Class A Star":
                return StarClass.A;
            case "Class B Star":
                return StarClass.B;
            case "Class O Star":
                return StarClass.O;
            default:
                Debug.LogWarning("Defaulted looking for star tag while saving...");
                return StarClass.M;
        }
    }
}
