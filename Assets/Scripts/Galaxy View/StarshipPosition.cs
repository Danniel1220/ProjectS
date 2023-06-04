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

    private PlanetInfoPanel planetInfoPanel;
    private PlanetMenuPanel planetMenuPanel;

    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject cachedTargetObject;
    [SerializeField] private int targetStarIndex;
    // this integer is equal to the planet index we are currently covering over
    // but it is equal to -1 if we are not currently hovering over a planet
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

        planetInfoPanel = UIManagers.planetInfoPanel;
        planetMenuPanel = UIManagers.planetMenuPanel;
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
        setTargetPositionViaStarSystem(starSystemGameObject);
    }

    public void setTargetPositionViaStarSystem(GameObject target)
    {
        // since we receive the star system game object right away, no need for a horrible mess of parenting logic, great
        targetObject = target;
        targetStarIndex = target.GetComponent<StarSystem>().index;
        // not hovering a planet, so this should be -1
        targetPlanetIndex = -1;
        // we're hovering a star so we dont need to cache anything
        cachedTargetObject = null;

        planetInfoPanel.closeWindow();
        planetMenuPanel.closeWindow();
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
        // if we move to a planet, we should cache the previous target too (the star's game object refference)
        // so that when we exit star system view we can set the target back to it without having to find it by index,
        // also this should only happen if the previous target was a star system (this is to prevent caching a planet when moving
        // from planet to planet)
        if (targetObject.tag == "Star System")
        {
            cachedTargetObject = targetObject;
        }

        Planet planetScript = target.GetComponent<Planet>();

        targetObject = target;
        targetPlanetIndex = planetScript.index;

        planetInfoPanel.openWindow();
        planetInfoPanel.updateInformation(planetScript);
    }

    public void enterStarSystemView()
    {
        // TODO: make moving stars relative to point to work properly
        //starFactory.moveStarSystemsRelativeToPoint(targetObject, true);
    }

    public void exitStarSystemView()
    {
        // if we are currently hovering over a planet
        if (targetPlanetIndex != -1)
        {
            // set the new target to be the one of the star we we're previously hovering over
            targetObject = cachedTargetObject;
            // means we dont need the cached target anymore
            cachedTargetObject = null;
            // signal that we are no longer hovering over a planet
            targetPlanetIndex = -1;

            // also disabling the relevant UI object since we know we no longer hover over anything
            // that we need a special panel for
            planetInfoPanel.gameObject.SetActive(false);
            planetMenuPanel.closeWindow();
        }
        // not hovering over a planet, so just exit the star system view as usual
        else
        {
            starFactory.moveStarSystemsRelativeToPoint(targetObject, false);
        }

        // TODO: make moving stars relative to point to work properly
        //starFactory.moveStarSystemsRelativeToPoint(targetObject, false);
    }

    public GameObject getTargetObject()
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
