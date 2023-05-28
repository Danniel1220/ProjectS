using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStarshipToChunkSystem : MonoBehaviour
{
    Transform starShipGameObjectTransform;
    Transform shipTransform;

    GalaxyChunkSystem galaxyChunkSystem;

    // Start is called before the first frame update
    void Start()
    {
        starShipGameObjectTransform = GetComponent<Transform>();
        shipTransform = this.transform.Find("Body").transform;

        galaxyChunkSystem = GameManagers.galaxyChunkSystem;
        galaxyChunkSystem.addItemToChunk(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        Debug.Log("STARSHIP DESTROYED");
    }
}
