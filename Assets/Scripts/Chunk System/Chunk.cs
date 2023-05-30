using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public Vector2 chunkPosition;
    public List<GameObject> chunkGameObjectList= new List<GameObject>();
    public Chunk(Vector2 chunkPosition)
    {
        this.chunkPosition = chunkPosition;
    }
}
