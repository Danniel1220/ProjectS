using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIcon : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI text;

    public void init()
    {
        background = this.transform.Find("Background").GetComponent<Image>();
        text = this.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }
}
