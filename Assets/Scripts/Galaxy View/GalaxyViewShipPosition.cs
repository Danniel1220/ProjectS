using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewShipPosition : MonoBehaviour
{
    Transform starShipGameObjectTransform;
    Transform shipTransform;
    Transform targetTransform;

    float shipSpeed = 2f;
    [SerializeField] private float floatDistanceAboveStar = 20f;

    // Start is called before the first frame update
    void Start()
    {
        starShipGameObjectTransform = GetComponent<Transform>();
        shipTransform = this.transform.Find("Body").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 targetPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + floatDistanceAboveStar, targetTransform.position.z);
            starShipGameObjectTransform.position = Vector3.Lerp(shipTransform.position, targetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));

        }
    }
    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
