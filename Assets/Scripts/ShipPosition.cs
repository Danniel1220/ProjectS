using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPosition : MonoBehaviour
{
    Transform shipTranform;
    Transform targetTransform;

    float shipSpeed = 420f;

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
            shipTranform.position = Vector3.MoveTowards(shipTranform.position, heightLockedTargetPosition, shipSpeed * Time.deltaTime);

        }
    }
    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
