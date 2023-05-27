using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewShipPosition : MonoBehaviour
{
    private GameObject starShipGameObject;
    private Transform shipTransform;

    [SerializeField ]private Transform targetTransform;

    float shipSpeed = 2f;
    [SerializeField] private float floatDistanceAboveTarget = 20f;

    StarFactory starHelper;

    // Start is called before the first frame update
    void Start()
    {
        starShipGameObject = this.gameObject;
        shipTransform = this.transform.Find("Body").transform;
        starHelper = GameObject.Find("StarManager").GetComponent<StarFactory>();
        targetTransform = GameObject.Find("Star Systems Container").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 targetPosition = new Vector3(targetTransform.transform.position.x, targetTransform.transform.position.y + floatDistanceAboveTarget, targetTransform.transform.position.z);
            starShipGameObject.transform.position = Vector3.Lerp(shipTransform.position, targetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));
            targetPosition.x= 0f;
        }
    }
    public void setTargetPosition(Transform target)
    {
        this.targetTransform = target;
        Debug.Log("setting target..");
    }

    public void enterStarView()
    {
        starHelper.moveStarSystemsRelativeToPoint(targetTransform, true);
    }

    public void exitStarView()
    {
        starHelper.moveStarSystemsRelativeToPoint(targetTransform, false);
    }

    public Transform getTargetPosition()
    {
        return targetTransform;
    }
}
