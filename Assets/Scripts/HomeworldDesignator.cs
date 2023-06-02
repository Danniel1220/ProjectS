using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworldDesignator : MonoBehaviour
{
    private ChunkSystem chunkSystem;
    private StarshipPosition starshipPosition;

    private GameObject homeworldStarSystem;

    void Start()
    {
        chunkSystem = GameManagers.chunkSystem;
        starshipPosition = GameManagers.starshipPosition;
    }

    public void designateHomeworld()
    {
        homeworldStarSystem = findHomeworldSystem();
        if (homeworldStarSystem == null)
        {
            Debug.LogError("Failed to assign a homeworld...");
        }
        else
        {
            // make the found star system homeworld
            StarSystem starSystemScript = homeworldStarSystem.GetComponent<StarSystem>();
            starSystemScript.makeHomeWorld();
            moveStarShipToHomeWorld();
        }
    }

    private GameObject findHomeworldSystem()
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
                    return starSystem;
                }
            }
        }

        return null;
    }

    public void moveStarShipToHomeWorld()
    {
        starshipPosition.transform.position = homeworldStarSystem.transform.position;
        starshipPosition.setTargetPositionViaStarSystem(homeworldStarSystem);
    }

    public GameObject getHomeworldStarSystem()
    {
        return homeworldStarSystem;
    }
}
