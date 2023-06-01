using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworldDesignator : MonoBehaviour
{
    ChunkSystem chunkSystem;
    GameObject homeworldStarSystem;
    

    // Start is called before the first frame update
    void Start()
    {
        chunkSystem = GameManagers.chunkSystem;
    }

    public void designateHomeWorld()
    {
        List<Chunk> chunks = chunkSystem.chunkList;
        // basically we are iterating through all star system to find one suitable for being a homeworld
        // G class star, like our sun, and 4 planets (this is arbitrary)
        foreach (Chunk chunk in chunks)
        {
            foreach (GameObject starSystem in chunk.chunkGameObjectList)
            {
                bool starIsGClass = false;
                int planetCount = 0;

                // iterate through the starsystem's children and find the one with tag == 'Star' meaning its the star object
                foreach (Transform child in starSystem.transform)
                {
                    // found a star that is class G
                    if (child.gameObject.tag == "Star" && child.Find("StarSphere").tag == "Class G Star")
                    {
                        starIsGClass = true;
                    }
                    // also count how many planets there are
                    if (child.gameObject.tag == "Planet")
                    {
                        planetCount++;
                    }
                }

                // if we found a G class star with more than 2 planets then it is a suitable homeworld
                if (starIsGClass == true && planetCount > 2)
                {
                    // we keep a refference to the homeworld system cached so we can move the starship there later
                    // without having to find it again
                    homeworldStarSystem = starSystem;

                    // make the star system homeworld
                    StarSystem starSystemScript = starSystem.GetComponent<StarSystem>();
                    starSystemScript.makeHomeWorld();

                    return;
                }
                else
                {
                    Debug.LogError("Could not find a suitable homeworld system...");
                }
            }
        }
    }

    public void moveStarShipToHomeWorld()
    {

    }
}
