using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickDetection : MonoBehaviour
{
    Transform objectTransform;
    StarshipPosition starshipPosition;

    void Start()
    {
        objectTransform = GetComponent<Transform>();
        starshipPosition = GameManagers.starshipPosition;
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse is over gameobject " + this.gameObject.name);
    }

    void OnMouseExit()
    {
        Debug.Log("mouse is no longer over gameobject " + this.gameObject.name);
    }

    void OnMouseDown()
    {
        // this prevents the click from happening if the cursor is currently over another gameObject that uses
        // the unity event system, so basically means we cant click through the UI since all of it
        // makes use of the event system
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("clicked gameobject " + this.gameObject.name);
            // for the star, the collider is placed on the star sphere instead of the star object so i need this
            // war crime here to fetch the parent.. too bad!
            if (this.gameObject.transform.parent.tag == "Star")
            {
                starshipPosition.setTargetPositionViaStar(objectTransform.gameObject);
            }
            else if (this.gameObject.tag == "Planet")
            {
                starshipPosition.setTargetPositionViaPlanet(objectTransform.gameObject);
            }
            else
            {
                // we should never get here but just in case because i dont trust myself
                Debug.LogError("Error in MouseClickDetection for game object with name: " + this.gameObject.name +
                    "\nDetected click on something with a tag that isn't accounted for.");
            }
        }
        else
        {
            Debug.LogWarning("Prevented click from going through to a star or planet!");
        }
        
    }
}
