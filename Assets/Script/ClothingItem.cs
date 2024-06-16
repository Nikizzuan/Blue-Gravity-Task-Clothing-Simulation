using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothingType
{
    Shirt,
    Costume,
    Pants,
    Shoes,
    Accessory
}

[CreateAssetMenu(fileName = "New Clothing", menuName = "Inventory/Clothing")]
public class ClothingItem : Item
{
    public ClothingType clothingType;

    public override void Use()
    {
        Debug.Log("Equipping clothing: " + itemName);
        // Implement clothing-specific behavior
    }
}