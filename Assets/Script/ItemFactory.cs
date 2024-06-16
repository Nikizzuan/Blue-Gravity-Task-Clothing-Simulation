using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory
{
    public static Item CreateItem(string itemType)
    {
        switch (itemType)
        {
            case "Clothing":
                return ScriptableObject.CreateInstance<ClothingItem>();
            default:
                throw new System.ArgumentException("Invalid item type");
        }
    }
}
