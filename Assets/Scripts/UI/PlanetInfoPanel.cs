using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlanetInfoPanel : MonoBehaviour
{
    private Inventory starshipInventory;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI informationText;
    private TextMeshProUGUI colonyPacksAvailableText;

    private Button colonizeButton;
    private Button openPlanetMenuButton;

    void Start()
    {
        starshipInventory = GameManagers.starshipInventory;

        nameText = this.gameObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        informationText = this.gameObject.transform.Find("InformationText").GetComponent<TextMeshProUGUI>();
        colonyPacksAvailableText = this.gameObject.transform.Find("ColonyPacksCountText").GetComponent<TextMeshProUGUI>();

        colonizeButton = this.gameObject.transform.Find("ColonizeButton").GetComponent<Button>();
        openPlanetMenuButton = this.gameObject.transform.Find("OpenPlanetMenuButton").GetComponent<Button>();
    }

    public void updatePlanetInfoPanel(string name, string information, bool isColonized)
    {
        nameText.text = name;
        informationText.text = information;
        colonyPacksAvailableText.text = "(" + starshipInventory.colonyPacks + " Colony Packs available)";

        // planet is colonized so we open the planet info tab accordingly with the planet menu button
        if (isColonized) 
        {
            colonizeButton.gameObject.SetActive(false);
            colonyPacksAvailableText.gameObject.SetActive(false);

            openPlanetMenuButton.gameObject.SetActive(true);
        }
        // otherwise open it with the colonization button
        else
        {
            openPlanetMenuButton.gameObject.SetActive(false);

            colonizeButton.gameObject.SetActive(true);
            colonyPacksAvailableText.gameObject.SetActive(true);
        }

    }
}
