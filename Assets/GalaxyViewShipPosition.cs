using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewShipPosition : MonoBehaviour
{
    Transform shipTranform;
    Transform targetTransform;

    float shipSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        shipTranform = this.transform.parent.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 heightLockedTargetPosition = new Vector3(targetTransform.position.x, shipTranform.position.y, targetTransform.position.z);
            shipTranform.position = Vector3.Lerp(shipTranform.position, heightLockedTargetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));

        }
    }
    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
