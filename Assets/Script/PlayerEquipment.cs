using UnityEngine;
using System.Collections.Generic;

public class EquippedClothing
{
    public GameObject gameObject;
    public ClothingItem clothingItem;

    public EquippedClothing(GameObject gameObject, ClothingItem clothingItem)
    {
        this.gameObject = gameObject;
        this.clothingItem = clothingItem;
    }
}
public class PlayerEquipment : MonoBehaviour
{
    public Transform shirtParent;
    public Transform pantsParent;
    public Transform shoesParent;
    public Transform accessoryParent;
    public Transform costumeParent;
    public Transform hairParent;

    // Player attachment animators
    public Animator _BaseAnimator;
    public Animator _HairAnimator;
    public Animator _AccessoriesAnimator;
    public Animator _PantsAnimator;
    public Animator _ClothesAnimator;
    public Animator _ShoesAnimator;
    public Animator _CostumeAnimator;

    private Dictionary<ClothingType, EquippedClothing> equippedItems = new Dictionary<ClothingType, EquippedClothing>();

    public void EquipItem(ClothingItem item)
    {
        if (item == null)
        {
            Debug.LogError("Clothing item is null");
            return;
        }

        if (item.clothingPrefab == null)
        {
            Debug.LogError("Clothing prefab is null for item: " + item.itemName);
            return;
        }

        Transform parentTransform = null;
        Animator animator = null;

        switch (item.clothingType)
        {
            case ClothingType.Shirt:
                parentTransform = shirtParent;
                animator = _ClothesAnimator;
                if (equippedItems.ContainsKey(ClothingType.Costume))
                    UnEquipItem(equippedItems[ClothingType.Costume].clothingItem);
                break;
            case ClothingType.Pants:
                parentTransform = pantsParent;
                animator = _PantsAnimator;
                if (equippedItems.ContainsKey(ClothingType.Costume))
                    UnEquipItem(equippedItems[ClothingType.Costume].clothingItem);
                break;
            case ClothingType.Shoes:
                parentTransform = shoesParent;
                animator = _ShoesAnimator;
                break;
            case ClothingType.Accessory:
                parentTransform = accessoryParent;
                animator = _AccessoriesAnimator;
                break;
            case ClothingType.Costume:
                parentTransform = costumeParent;
                animator = _CostumeAnimator;
                if (equippedItems.ContainsKey(ClothingType.Shirt))
                    UnEquipItem(equippedItems[ClothingType.Shirt].clothingItem);
                if (equippedItems.ContainsKey(ClothingType.Pants))
                    UnEquipItem(equippedItems[ClothingType.Pants].clothingItem);
                _ClothesAnimator = null;
                _PantsAnimator = null;
                break;
            case ClothingType.Hair:
                parentTransform = hairParent;
                animator = _HairAnimator;
                break;
        }

        // Destroy the current equipped item of the same type
        if (equippedItems.ContainsKey(item.clothingType))
        {
            Destroy(equippedItems[item.clothingType].gameObject);
        }

        // Equip the new item
        GameObject newEquippedItem = Instantiate(item.clothingPrefab, parentTransform);
        animator = newEquippedItem.GetComponent<Animator>();

        equippedItems[item.clothingType] = new EquippedClothing(newEquippedItem, item);

        // Update specific animator references
        if (item.clothingType == ClothingType.Shirt)
            _ClothesAnimator = animator;
        else if (item.clothingType == ClothingType.Pants)
            _PantsAnimator = animator;
        else if (item.clothingType == ClothingType.Shoes)
            _ShoesAnimator = animator;
        else if (item.clothingType == ClothingType.Accessory)
            _AccessoriesAnimator = animator;
        else if (item.clothingType == ClothingType.Costume)
            _CostumeAnimator = animator;
        else if (item.clothingType == ClothingType.Hair)
            _HairAnimator = animator;
    }


    public void UnEquipItem(ClothingItem item)
    {
        if (item == null)
        {
            Debug.LogError("Clothing item is null");
            return;
        }

        if (!equippedItems.ContainsKey(item.clothingType))
        {
            Debug.LogWarning("No item of type " + item.clothingType + " is currently equipped");
            return;
        }

        // Destroy the equipped item game object
        Destroy(equippedItems[item.clothingType].gameObject);

        // Remove the item from the dictionary
        equippedItems.Remove(item.clothingType);

        // Reset the specific animator reference if necessary
        switch (item.clothingType)
        {
            case ClothingType.Shirt:
                _ClothesAnimator = null;
                break;
            case ClothingType.Pants:
                _PantsAnimator = null;
                break;
            case ClothingType.Shoes:
                _ShoesAnimator = null;
                break;
            case ClothingType.Accessory:
                _AccessoriesAnimator = null;
                break;
            case ClothingType.Costume:
                _CostumeAnimator = null;
                break;
            case ClothingType.Hair:
                _HairAnimator = null;
                break;
        }
    }


    public bool IsItemEquipped(ClothingItem item)
    {
        return equippedItems.ContainsKey(item.clothingType) && equippedItems[item.clothingType].clothingItem == item;
    }


}
