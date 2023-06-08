using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SectorsStats : MonoBehaviour
{
    public TextMeshProUGUI habitat;
    public TextMeshProUGUI storage;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI mining;
    public TextMeshProUGUI production;
    public TextMeshProUGUI science;

    void Start()
    {
        habitat = this.transform.Find("Habitat").Find("Text").GetComponent<TextMeshProUGUI>();
        storage = this.transform.Find("Storage").Find("Text").GetComponent<TextMeshProUGUI>();
        energy = this.transform.Find("Energy").Find("Text").GetComponent<TextMeshProUGUI>();
        mining = this.transform.Find("Mining").Find("Text").GetComponent<TextMeshProUGUI>();
        production = this.transform.Find("Production").Find("Text").GetComponent<TextMeshProUGUI>();
        science = this.transform.Find("Science").Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void setStats(int habitat, int storage, int energy, int mining, int production, int science)
    {
        this.habitat.text = habitat.ToString();
        this.storage.text = storage.ToString();
        this.energy.text = energy.ToString();
        this.mining.text = mining.ToString();
        this.production.text = production.ToString();
        this.science.text = science.ToString();
    }
}
