using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GalaxySaver : MonoBehaviour
{
    GalaxyChunkSystem galaxyChunkSystem;
    List<GalaxyChunk> chunkList;
    public bool save = false;
    // Start is called before the first frame update
    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();
        chunkList = galaxyChunkSystem.getAllChunks();
    }

    // Update is called once per frame
    void Update()
    {
        if (save)
        {
            Debug.Log(galaxyChunkSystem);
            /*foreach (GalaxyChunk chunk in chunkList)
            {
                foreach (GameObject star in chunk.chunkGameObjectList)
                {
                    GalaxyViewStarJsonIntermediary jsonIntermediary = new GalaxyViewStarJsonIntermediary(
                        star.transform.position.x, 
                        star.transform.position.y, 
                        star.transform.position.z);

                    string json = JsonUtility.ToJson(jsonIntermediary);
                    Debug.Log(json);
                }
            }*/
            GameObject star = chunkList[0].chunkGameObjectList[0];
            string starTag = star.tag;
            StarClass starClass;
            switch (starTag)
            {
                case "M Class Star":
                    starClass = StarClass.M;
                    break;
                case "K Class Star":
                    starClass = StarClass.K;
                    break;
                case "G Class Star":
                    starClass = StarClass.G;
                    break;
                case "F Class Star":
                    starClass = StarClass.F;
                    break;
                case "A Class Star":
                    starClass = StarClass.A;
                    break;
                case "B Class Star":
                    starClass = StarClass.B;
                    break;
                case "O Class Star":
                    starClass = StarClass.O;
                    break;
                default: 
                    starClass = StarClass.M; 
                    break;
            }

            GalaxyViewStarJsonIntermediary jsonIntermediary = new GalaxyViewStarJsonIntermediary(
                        star.transform.position.x,
                        star.transform.position.y,
                        star.transform.position.z,
                        starClass);
            string json = JsonUtility.ToJson(jsonIntermediary);
            Debug.Log(json);
            save = false;
        }
    }
}
