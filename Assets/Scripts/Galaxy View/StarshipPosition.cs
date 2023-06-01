using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipPosition : MonoBehaviour
{
    private GameObject starShipGameObject;
    private Transform shipTransform;

    [SerializeField] private GameObject targetObject;

    float shipSpeed = 2f;
    [SerializeField] private float floatDistanceAboveTarget = 20f;

    StarFactory starFactory;

    // Start is called before the first frame update
    void Start()
    {
        starShipGameObject = this.gameObject;
        shipTransform = this.transform.Find("Body").transform;
        starFactory = GameManagers.starFactory;
        targetObject = GameObject.Find("Star Systems Container").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + floatDistanceAboveTarget, targetObject.transform.position.z);
            starShipGameObject.transform.position = Vector3.Lerp(shipTransform.position, targetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));
            targetPosition.x= 0f;
        }
    }
    public void setTargetPosition(GameObject target)
    {
        // this ugly mess is required because the collider that checks for a click is bound to
        // the StarSphere object of a star, but we actually need to check for the whole star system
        // so the parent of the parent of the star sphere is actually the whole system and we retrieve that here

        // this shit is absolutely horrendous... too bad!
        this.targetObject = target.transform.parent.gameObject.transform.parent.gameObject;
        Debug.Log("setting target..");
    }

    public void enterStarView()
    {
        starFactory.moveStarSystemsRelativeToPoint(targetObject, true);
    }

    public void exitStarView()
    {
        starFactory.moveStarSystemsRelativeToPoint(targetObject, false);
    }

    public GameObject getTargetStarSystem()
    {
        return targetObject;
    }
}
