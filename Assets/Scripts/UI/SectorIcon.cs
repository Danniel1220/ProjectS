using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectorIcon : MonoBehaviour
{
    PlanetMenu planetMenuPanel;

    private Button button;
    private Image buttonImage;
    private TextMeshProUGUI text;
    private Image progressBarBackground;
    private Image progressBarFill;

    public bool isEnabled;

    void Start()
    {
        planetMenuPanel = UIManagers.planetMenuPanel;

        button = this.transform.Find("Button").GetComponent<Button>();
        buttonImage = this.transform.Find("Button").GetComponent<Image>();
        text = this.transform.Find("Button").Find("Text").GetComponent<TextMeshProUGUI>();
        progressBarBackground = this.transform.Find("ProgressBar").transform.Find("Background").GetComponent<Image>();
        progressBarFill = this.transform.Find("ProgressBar").transform.Find("Fill").GetComponent<Image>();
    }

    public void setObjectAlpha(float alpha)
    {
        // replaces all the colors with identical ones with the given argument alpha
        // the reason why i have to create new colors is because i cant just change the alpha value
        // because it is read-only

        buttonImage.color = new Color(
            buttonImage.color.r,
            buttonImage.color.g,
            buttonImage.color.b,
            alpha);
        text.color = new Color(
            text.color.r,
            text.color.g,
            text.color.b,
            alpha);
        progressBarBackground.color = new Color(
            progressBarBackground.color.r,
            progressBarBackground.color.g,
            progressBarBackground.color.b,
            alpha);
        progressBarFill.color = new Color(
            progressBarFill.color.r,
            progressBarFill.color.g,
            progressBarFill.color.b,
            alpha);
    }

    public void setProgressBarValue(float value)
    {
        progressBarFill.fillAmount = value;
    }

    public void selectSector()
    {
        if (isEnabled)
        {
            planetMenuPanel.clearBuildingsGrid();

            // every sector icon button calls this function, and depending on the parent's name (the object this was called from)
            // the appropiate sector's display function gets called
            switch (this.gameObject.name)
            {
                case "Habitat":
                    planetMenuPanel.displayHabitatBuildings();
                    break;
                case "Storage":
                    planetMenuPanel.displayStorageBuildings();
                    break;
                case "Energy":
                    planetMenuPanel.displayEnergyBuildings();
                    break;
                case "Mining":
                    planetMenuPanel.displayMiningBuildings();
                    break;
                case "Production":
                    planetMenuPanel.displayProductionBuildings();
                    break;
                case "Science":
                    planetMenuPanel.displayScienceBuildings();
                    break;
            }
        }
        else
        {
            // sending the name of the sector as parameter
            planetMenuPanel.addSectorToPlanet(this.gameObject.name);
        }
    }
}
