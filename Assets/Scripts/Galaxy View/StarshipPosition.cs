using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipPosition : MonoBehaviour
{
    private GameObject starshipGameObject;
    private Transform starshipTransform;
    private HomeworldDesignator homeworldDesignator;
    private ChunkSystem chunkSystem;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private int targetStarIndex;
    [SerializeField] private int targetPlanetIndex;

    float shipSpeed = 2f;
    [SerializeField] private float floatDistanceAboveTarget = 20f;

    StarFactory starFactory;

    // Start is called before the first frame update
    void Start()
    {
        starshipGameObject = this.gameObject;
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
            starshipGameObject.transform.position = Vector3.Lerp(starshipTransform.position, targetPosition, Mathf.MoveTowards(0f, 1f, shipSpeed * Time.deltaTime));
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
        targetStarIndex = starSystemGameObject.GetComponent<StarSystem>().index;
    }

    public void setTargetPositionViaStarSystem(GameObject target)
    {
        // since we receive the star system game object right away, no need for a horrible mess of parenting logic, great
        targetObject = target;
        targetStarIndex = target.GetComponent<StarSystem>().index;
    }

    // this isn't particularly efficient so it is only used when loading the save file
    // but it is way more efficient than looking for a star system via name so it will do
    public void setTargetViaStarSystemIndex(int index)
    {
        setTargetPositionViaStarSystem(chunkSystem.getStarSystemViaIndex(index));
    }

    // if we received a command to set the target to a planet then it should mean we already have a target solar system to our ship
    // so we should also keep track of the system in the background so that when we exit the star system view we bind the transform
    // back to the star itself rather than the planet
    public void setTargetPositionViaPlanet(GameObject target)
    {
        targetObject = target;
        targetPlanetIndex = target.GetComponent<Planet>().index;
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

    public int getTargetIndex()
    {
        return targetStarIndex;
    }
}
