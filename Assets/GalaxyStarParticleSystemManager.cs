using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyStarParticleSystemManager : MonoBehaviour
{
    GalaxyChunkSystem galaxyChunkSystem;
    GameObject starShip;
    GalaxyChunk starShipCurrentChunk;
    GalaxyChunk starShipPreviousChunk;
    List<GalaxyChunk> chunksWithParticlesEnabled = new List<GalaxyChunk>();
    // Start is called before the first frame update
    void Start()
    {
        galaxyChunkSystem = GameObject.Find("GalaxyChunkGrid").GetComponent<GalaxyChunkSystem>();
        starShip = GameObject.Find("Starship");
        starShipCurrentChunk = galaxyChunkSystem.getChunkOfGameObject(starShip);
        starShipPreviousChunk = starShipCurrentChunk;

        chunksWithParticlesEnabled.Add(starShipCurrentChunk);
        chunksWithParticlesEnabled.AddRange(galaxyChunkSystem.getAdjacentChunks(starShipCurrentChunk));
    }

    // Update is called once per frame
    void Update()
    {
        // if the starship moved chunks since the last frame
        if (starShipCurrentChunk != starShipPreviousChunk)
        {

        }
    }
}
