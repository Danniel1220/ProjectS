using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewStarMouseDetection : MonoBehaviour
{
    Transform starTransform;
    GalaxyViewShipPosition shipPosition;

    // Start is called before the first frame update
    void Start()
    {
        starTransform = GetComponent<Transform>();
        shipPosition = GameObject.Find("Starship").transform.Find("Body").GetComponent<GalaxyViewShipPosition>();
    }

    // Update is called once per frame
    void Update()
    {

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
        shipPosition.SetTargetTransform(starTransform);
    }
}
