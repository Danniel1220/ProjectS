using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyChunk
{
    public Vector2 chunkPosition;
    public List<GameObject> chunkGameObjectList= new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GalaxyChunk(Vector2 chunkPosition)
    {
        this.chunkPosition = chunkPosition;
    }
}
