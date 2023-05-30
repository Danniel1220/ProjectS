using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkSystem : MonoBehaviour
{
    private float chunkSize = 10240f;
    public float ChunkSize { get => chunkSize; }

    private Dictionary<Vector2, Chunk> chunkDictionary = new Dictionary<Vector2, Chunk>();

    public List<Chunk> chunkList = new List<Chunk>();

    public Text UIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = getTotalNumberOfGameObjectsInChunks().ToString();
    }

    public void addItemToChunk(GameObject item)
    {
        Vector2 chunkPosition = parseGameObjectPositionToChunkPosition(item);

        if (chunkDictionary.ContainsKey(chunkPosition) == false)
        {
            Debug.Log("Creating new chunk: " + chunkPosition.x + " " + chunkPosition.y);
            Chunk newChunk = new Chunk(chunkPosition);
            chunkDictionary.Add(chunkPosition, newChunk);
            chunkList.Add(newChunk);
        }

        chunkDictionary[chunkPosition].chunkGameObjectList.Add(item);
    }

    private Vector2 parseGameObjectPositionToChunkPosition(GameObject gameObject)
    {
        float positionX = gameObject.transform.position.x;
        float positionZ = gameObject.transform.position.z;

        positionX = positionX / chunkSize;
        positionZ = positionZ / chunkSize;

        if (positionX >= 0) positionX = Mathf.Floor(positionX);
        else positionX = Mathf.Ceil(positionX);

        if (positionZ >= 0) positionZ = Mathf.Floor(positionZ);
        else positionZ = Mathf.Ceil(positionZ);

        return new Vector2(positionX * chunkSize, positionZ * chunkSize);
    }

    public Chunk getGalaxyChunk(Vector2 chunkPosition)
    {
        if (chunkDictionary.ContainsKey(chunkPosition)) return chunkDictionary[chunkPosition];
        else return null;
    }

    public List<Chunk> getAdjacentChunks(Chunk chunk)
    {
        List<Chunk> adjacentChunks = new List<Chunk>();

        // Vector2 uses X and Y as it's coordinates, but our chunks are described based on X and Z in world space
        // as such, when checking if the dictionary contains a key and we construct a new Vector2
        // the Y coordinate is actually Z in worldspace

        // grabbing chunks below: (x-1, z-1), (x, z-1), (x+1, z-1)
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y - 1))) 
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y - 1)]);
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x, chunk.chunkPosition.y - 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x, chunk.chunkPosition.y - 1)]);
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y - 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y - 1)]);

        // grabbing chunks from left and right: (x-1, z), (x+1, z)
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y)]);
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y - 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y - 1)]);

        // grabbing chunks from above: (x-1, z+1), (x, z+1), (x+1, z+1)
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y + 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x - 1, chunk.chunkPosition.y + 1)]);
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x, chunk.chunkPosition.y + 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x, chunk.chunkPosition.y + 1)]);
        if (chunkDictionary.ContainsKey(new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y + 1)))
            adjacentChunks.Add(chunkDictionary[new Vector2(chunk.chunkPosition.x + 1, chunk.chunkPosition.y + 1)]);

        return adjacentChunks;
    }

    public void deleteGameObjectFromChunk(GameObject gameObject, Chunk chunk)
    {
        if (chunkDictionary.ContainsKey(chunk.chunkPosition))
        {
            chunkDictionary[chunk.chunkPosition].chunkGameObjectList.Remove(gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Attempted to delete a GameObject from a chunk that does not exist.\n" +
                "GameObject: " + gameObject + "\n" + 
                "GalaxyChunk: " + chunk);
        }
    }

    public float getTotalNumberOfGameObjectsInChunks()
    {
        float total = 0f;
        foreach (Chunk chunk in chunkDictionary.Values) 
        {
            total = total + chunk.chunkGameObjectList.Count;
        }
        return total;
    }

    public List<Chunk> getAllChunks()
    {
        return chunkList;
    }

    public Chunk getChunkOfGameObject(GameObject gameObject)
    {
        Vector2 gameObjectChunkPosition = parseGameObjectPositionToChunkPosition(gameObject);

        if (chunkDictionary.ContainsKey(gameObjectChunkPosition))
        {
            return chunkDictionary[gameObjectChunkPosition];
        }
        return null;
    }

    public Vector2 getChunkCenter(Chunk chunk)
    {
        return new Vector2(chunk.chunkPosition.x + chunkSize / 2, chunk.chunkPosition.y + chunkSize / 2);
    }
}
