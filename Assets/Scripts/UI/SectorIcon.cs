using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectorIcon : MonoBehaviour
{
    private Image iconBackground;
    private TextMeshProUGUI text;
    private Image progressBarBackground;
    private Image progressBarFill;

    void Start()
    {
        iconBackground = this.transform.Find("Background").GetComponent<Image>();
        text = this.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        progressBarBackground = this.transform.Find("ProgressBar").transform.Find("Background").GetComponent<Image>();
        progressBarFill = this.transform.Find("ProgressBar").transform.Find("Fill").GetComponent<Image>();
    }

    public void setObjectAlpha(float alpha)
    {
        // replaces all the colors with identical ones with the given argument alpha
        // the reason why i have to create new colors is because i cant just change the alpha value
        // because it is read-only
        iconBackground.color = new Color(
            iconBackground.color.r,
            iconBackground.color.g,
            iconBackground.color.b,
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
}
