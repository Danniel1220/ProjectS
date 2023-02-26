using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewScaleStarByDistanceToCamera : MonoBehaviour
{
    GalaxyChunkSystem galaxyChunkSystem;
    List<GalaxyChunk> galaxyChunks;
    GameObject starShip;
    Camera camera;

    [SerializeField] private float smallScale = 20f;
    [SerializeField] private float bigScale = 200f;
    [SerializeField] private float scaleDistance = 20000f;
    [SerializeField] private bool update;

    void Start()
    {
        camera = Camera.main;
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();
        starShip = GameObject.Find("Starship");
    }

    void Update()
    {
        List<GameObject> smallStars = new List<GameObject>();
        List<GameObject> bigStars = new List<GameObject>();

        galaxyChunks = galaxyChunkSystem.getAllChunks();
        foreach (GalaxyChunk chunk in galaxyChunks)
        {
            Vector2 chunkCenter = galaxyChunkSystem.getChunkCenter(chunk);
            if (Vector3.Distance(camera.transform.position, new Vector3(chunkCenter.x, 0, chunkCenter.y)) > scaleDistance)
            {
                foreach (GameObject star in chunk.chunkGameObjectList)
                {
                    bigStars.Add(star);
                }
            }
            else
            {
                foreach (GameObject star in chunk.chunkGameObjectList)
                {
                    smallStars.Add(star);
                }
            }
        }

        foreach (GameObject star in bigStars)
        {
            if (star.tag == "Galaxy View Star")
            {
                star.gameObject.transform.GetChild(0).Find("StarSphere").transform.localScale = new Vector3(bigScale, bigScale, bigScale);
            }
        }
        foreach (GameObject star in smallStars)
        {
            if (star.tag == "Galaxy View Star")
            {
                star.gameObject.transform.GetChild(0).Find("StarSphere").transform.localScale = new Vector3(smallScale, smallScale, smallScale);

            }
        }  
    }
}
