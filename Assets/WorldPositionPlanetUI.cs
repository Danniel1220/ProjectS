using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldPositionPlanetUI : MonoBehaviour
{
    private Transform targetTransform;

    private bool isTargetVisible;

    public Image targetImage;

    // Start is called before the first frame update
    void Start()
    {
        isTargetVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTargetVisible) targetImage.enabled = true;
        else targetImage.enabled = false;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
        targetImage.transform.position = screenPoint;
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void ShowTarget()
    {
        isTargetVisible = true;
    }

    public void HideTarget()
    {
        isTargetVisible = false;
    }
}
