using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    PlanetSelectionTooltipManager planetSelectionToolTipManager;
    Transform planetTransform;
    ShipPosition shipPosition;

    // Start is called before the first frame update
    void Start()
    {
        planetSelectionToolTipManager = GameObject.Find("Canvas").transform.Find("SelectionMark").GetComponent<PlanetSelectionTooltipManager>();
        planetTransform = GetComponent<Transform>();
        shipPosition = GameObject.Find("Starship").transform.Find("Body").GetComponent<ShipPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse is over gameobject " + this.gameObject.name);
        planetSelectionToolTipManager.SetTargetTransform(planetTransform);
        planetSelectionToolTipManager.ShowTarget();
    }

    void OnMouseExit()
    {
        Debug.Log("mouse is no longer over gameobject " + this.gameObject.name);
        planetSelectionToolTipManager.HideTarget();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked gameobject " + this.gameObject.name);
        shipPosition.SetTargetTransform(planetTransform);
    }
}
