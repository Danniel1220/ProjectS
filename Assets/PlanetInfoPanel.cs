using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI informationText;
    void Start()
    {
        nameText = this.gameObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        informationText = this.gameObject.transform.Find("InformationText").GetComponent<TextMeshProUGUI>();
    }

    public void setNameText(string text)
    {
        this.nameText.text = text;
    }

    public void setInformationText(string text)
    {
        this.informationText.text = text;
    }
}
