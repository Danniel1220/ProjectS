using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int index;
    public List<Resource> resources;
    public string planetInfo;
    public bool isColonized;

    public void init()
    {
        initPlanetInfo();
        initResources();
    }

    private void initPlanetInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Resource resource in resources)
        {
            sb.Append(resource.name);
            sb.Append(" (");
            sb.Append(resource.percentageByPlanetWeight * 100);
            sb.Append("%)\n");
        }
        planetInfo = sb.ToString();
    }

    private void initResources()
    {
        List<Resource> resources = new List<Resource>();

        float ironMin = 0.3f;
        float ironMax = 0.35f;
        float iron = Mathf.Round(Random.Range(ironMin, ironMax) * 10000) / 10000f;
        Debug.Log(iron);

        float oxygenMin = 0.28f;
        float oxygenMax = 0.32f;
        float oxygen = Mathf.Round(Random.Range(oxygenMin, oxygenMax) * 10000) / 10000;

        float silliconMin = 0.148f;
        float silliconMax = 0.152f;
        float sillicon = Mathf.Round(Random.Range(silliconMin, silliconMax) * 10000) / 10000;

        float magnesiumMin = 0.138f;
        float magnesiumMax = 0.142f;
        float magnesium = Mathf.Round(Random.Range(magnesiumMin, magnesiumMax) * 10000) / 10000;

        float sulfurMin = 0.026f;
        float sulfurMax = 0.032f;
        float sulfur = Mathf.Round(Random.Range(sulfurMin, sulfurMax) * 10000) / 10000;

        float nickelMin = 0.014f;
        float nickelMax = 0.020f;
        float nickel = Mathf.Round(Random.Range(nickelMin, nickelMax) * 10000) / 10000;

        float calciumMin = 0.012f;
        float calciumMax = 0.016f;
        float calcium = Mathf.Round(Random.Range(calciumMin, calciumMax) * 10000) / 10000;

        float aluminumMin = 0.012f;
        float aluminumMax = 0.016f;
        float aluminum = Mathf.Round(Random.Range(aluminumMin, aluminumMax) * 10000) / 10000;

        float traceMin = 0.010f;
        float traceMax = 0.014f;
        float trace = Mathf.Round(Random.Range(traceMin, traceMax) * 10000) / 10000;

        resources.Add(new Resource("Iron", "Fe", iron));
        resources.Add(new Resource("Oxygen", "O", oxygen));
        resources.Add(new Resource("Sillicon", "Si", sillicon));
        resources.Add(new Resource("Magnesium", "Mg", magnesium));
        resources.Add(new Resource("Sulfur", "S", sulfur));
        resources.Add(new Resource("Nickel", "Ni", nickel));
        resources.Add(new Resource("Calcium", "Fe", calcium));
        resources.Add(new Resource("Aluminum", "Fe", aluminum));
        resources.Add(new Resource("Trace Elements", "Trace", trace));

        this.resources = resources;
    }
}
