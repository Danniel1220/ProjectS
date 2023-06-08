using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePanel : MonoBehaviour
{
    GameObject storageScrollViewContents;
    GameObject itemUIPrefab;

    void Start()
    {
        storageScrollViewContents = this.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
        itemUIPrefab = Resources.Load("Prefabs/ItemUIPrefab") as GameObject;
    }

    public void addItem(Item item, int amount)
    {
        GameObject itemUI = Instantiate(itemUIPrefab);
        itemUI.transform.SetParent(storageScrollViewContents.transform, false);
        ItemUI itemUIScript = itemUI.GetComponent<ItemUI>();
        itemUIScript.init(item, amount);
    }

    public void clearItems()
    {
        foreach(Transform item in storageScrollViewContents.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
