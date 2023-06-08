using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public static Item Raw_Resource;

    void Start()
    {
        Raw_Resource = Resources.Load("Items/Raw Resource") as Item;
    }

    public static Item getItemById(int id)
    {
        switch (id)
        {
            case 1: return Raw_Resource;
        }
        Debug.LogError("Invalid id requested from ItemObject static function getItemById() with id = " + id);
        return null;
    }
}
