using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
