using UnityEngine;

public enum ClothingType
{
    Shirt,
    Costume,
    Pants,
    Shoes,
    Accessory,
    Hair
}

[CreateAssetMenu(fileName = "New Clothing", menuName = "Inventory/Clothing")]
public class ClothingItem : Item
{
    public ClothingType clothingType;
    public GameObject clothingPrefab; // Reference to the clothing prefab

    public override void Use()
    {
        Debug.Log("Equipping clothing: " + itemName);
        // Implement clothing-specific behavior
    }
}
