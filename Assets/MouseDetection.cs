using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    WorldPositionPlanetUI worldPositionPlanetUIScript;
    // Start is called before the first frame update
    void Start()
    {
        worldPositionPlanetUIScript = GetComponent<WorldPositionPlanetUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse is over gameobject " + this.gameObject.name);
        worldPositionPlanetUIScript.SetTargetTransform(transform);
        worldPositionPlanetUIScript.ShowTarget();
    }

    void OnMouseExit()
    {
        Debug.Log("mouse is no longer over gameobject " + this.gameObject.name);
        worldPositionPlanetUIScript.HideTarget();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked gameobject " + this.gameObject.name);
    }
}
