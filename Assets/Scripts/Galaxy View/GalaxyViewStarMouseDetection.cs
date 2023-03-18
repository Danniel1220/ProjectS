using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewStarMouseDetection : MonoBehaviour
{
    Transform starTransform;
    GalaxyViewShipPosition galaxyViewShipPosition;

    // Start is called before the first frame update
    void Start()
    {
        starTransform = GetComponent<Transform>();
        galaxyViewShipPosition = GameObject.Find("Starship").GetComponent<GalaxyViewShipPosition>();
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
        galaxyViewShipPosition.setTargetPosition(starTransform.parent.transform);
    }
}
