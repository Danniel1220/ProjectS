using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSelectionTooltipManager : MonoBehaviour
{
    Image selectionImage;

    private Transform targetTransform;

    private bool isTargetVisible;

    // Start is called before the first frame update
    void Start()
    {
        selectionImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetVisible) UpdateSelectionImagePositionToScreenPoint();
    }

    public void UpdateSelectionImagePositionToScreenPoint()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
        selectionImage.transform.position = screenPoint;
    }

    public void ShowTarget()
    {
        isTargetVisible = true;
        selectionImage.enabled = true;
    }

    public void HideTarget()
    {
        isTargetVisible = false;
        selectionImage.enabled = false;
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
