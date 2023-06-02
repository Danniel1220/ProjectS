using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipPosition : MonoBehaviour
{
    private Transform starshipTransform;
    private HomeworldDesignator homeworldDesignator;
    private ChunkSystem chunkSystem;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private int targetIndex;

    float shipSpeed = 2f;
    [SerializeField] private float floatDistanceAboveTarget = 20f;

    StarFactory starFactory;

    // Start is called before the first frame update
    void Start()
    {
        // refference to the transform of the whole starship gameobject
        starshipTransform = this.transform.Find("Body").transform;
        starFactory = GameManagers.starFactory;
        homeworldDesignator = GameManagers.homeworldDesignator;
        chunkSystem = GameManagers.chunkSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + floatDistanceAboveTarget, targetObject.transform.position.z);
            starshipTransform.position = Vector3.Lerp(starshipTransform.position, targetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));
            targetPosition.x = 0f;
        }
    }
    public void setTargetPositionViaStar(GameObject target)
    {
        // this ugly mess is required because the collider that checks for a click is bound to
        // the StarSphere object of a star, but we actually need to refference the whole star system
        // so the parent of the parent of the star sphere is actually the whole system and thats what we are setting the target to

        // also, this shit is absolutely horrendous... too bad!
        GameObject starSystemGameObject = target.transform.parent.gameObject.transform.parent.gameObject;

        targetObject = starSystemGameObject;
        targetIndex = starSystemGameObject.GetComponent<StarSystem>().index;
    }

    public void setTargetPositionViaStarSystem(GameObject target)
    {
        // since we receive the star system game object right away, no need for a horrible mess of parenting logic, great
        targetObject = target;
        targetIndex = target.GetComponent<StarSystem>().index;
    }

    // this isn't particularly efficient so it is only used when loading the save file
    // but it is way more efficient than looking for a star system via name so it will do
    public void setTargetViaStarSystemIndex(int index)
    {
        setTargetPositionViaStarSystem(chunkSystem.getStarSystemViaIndex(index));
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

    public void setTargetToHomeworldSystem()
    {
        starshipTransform.position = homeworldDesignator.getHomeworldStarSystem().transform.position;
        setTargetPositionViaStarSystem(homeworldDesignator.getHomeworldStarSystem());
    }

    public Transform getStarshipTransform()
    {
        return starshipTransform;
    }

    // this is kinda ~not nice~ because we dont update the target index too by doing this but.. too bad!
    public void setStarshipPositionViaVector3(Vector3 coordinates)
    {
        starshipTransform.position = coordinates;
    }
}
