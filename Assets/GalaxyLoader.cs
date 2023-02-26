using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GalaxyLoader : MonoBehaviour
{
    GalaxyViewStarDataWrapper dataWrapper;
    GalaxyChunkSystem galaxyChunkSystem;
    GalaxyStarGenerator galaxyStarGenerator;

    [SerializeField] private GameObject classMStarPrefab;
    [SerializeField] private GameObject classKStarPrefab;
    [SerializeField] private GameObject classGStarPrefab;
    [SerializeField] private GameObject classFStarPrefab;
    [SerializeField] private GameObject classAStarPrefab;
    [SerializeField] private GameObject classBStarPrefab;
    [SerializeField] private GameObject classOStarPrefab;

    public float starScale;

    // Start is called before the first frame update
    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();
        galaxyStarGenerator = GameObject.Find("Galaxy").GetComponent<GalaxyStarGenerator>();
        dataWrapper = deserializeJsonFile(Application.dataPath + "/StarLocations.json");

        foreach (GalaxyViewStarSerializableData star in dataWrapper.starData)
        {
            // creating the point in space
            GameObject point = new GameObject();

            point.transform.position = new Vector3(star.posX, star.posY, star.posZ);
            point.transform.rotation = Quaternion.identity;
            point.name = "Star";
            point.tag = "Galaxy View Star";
            galaxyChunkSystem.addItemToChunk(point);
            createStar(point, star);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GalaxyViewStarDataWrapper deserializeJsonFile(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GalaxyViewStarDataWrapper>(json);
    }

    public GalaxyViewStarDataWrapper deserializeBinaryFile(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        string data = File.ReadAllText(path);
        return null;
    }



    public void createStar(GameObject parent, GalaxyViewStarSerializableData starData)
    {
        GameObject star;
        switch(starData.starClass)
        {
            case "M":
                star = Instantiate(classMStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "K":
                star = Instantiate(classKStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "G":
                star = Instantiate(classGStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "F":
                star = Instantiate(classFStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "A":
                star = Instantiate(classAStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "B":
                star = Instantiate(classBStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            case "O":
                star = Instantiate(classOStarPrefab, parent.transform.position, Quaternion.identity);
                break;
            default:
                star = Instantiate(classMStarPrefab, parent.transform.position, Quaternion.identity);
                Debug.LogWarning("Defaulted on creating star while loading...");
                break;
        }
        star.transform.parent = parent.transform;
        if (starScale != 0) star.transform.Find("StarSphere").transform.localScale = new Vector3(starScale, starScale, starScale);

    }
}
