using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    private Item item;

    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI amountText;
    private TextMeshProUGUI sendAmountInputText;

    void Awake()
    {
        itemNameText = this.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        amountText = this.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
        sendAmountInputText = this.transform.Find("PlanetSendAmount").Find("Text Area").Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void init(Item item, int amount)
    {
        this.item = item;

        itemNameText.text = item.itemName;
        amountText.text = amount.ToString("N0"); // this formats the string to have thousands sepparators
    }

    public void sendToPlanet()
    {

    }    
}
