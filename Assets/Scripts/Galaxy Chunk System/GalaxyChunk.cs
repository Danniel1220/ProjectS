using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyChunk
{
    public Vector2 chunkPosition;
    public List<GameObject> chunkGameObjectList= new List<GameObject>();
    public GalaxyChunk(Vector2 chunkPosition)
    {
        this.chunkPosition = chunkPosition;
    }
}
