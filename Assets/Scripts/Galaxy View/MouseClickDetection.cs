using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("clicked gameobject " + this.gameObject.name);
        if (this.gameObject.tag == "Star")
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
}
