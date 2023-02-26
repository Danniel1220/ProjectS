using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewShipPosition : MonoBehaviour
{
    Transform starShipGameObjectTransform;
    Transform shipTransform;
    Transform targetTransform;

    float shipSpeed = 2f;

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
            Vector3 heightLockedTargetPosition = new Vector3(targetTransform.position.x, shipTransform.position.y, targetTransform.position.z);
            starShipGameObjectTransform.position = Vector3.Lerp(shipTransform.position, heightLockedTargetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));

        }
    }
    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
