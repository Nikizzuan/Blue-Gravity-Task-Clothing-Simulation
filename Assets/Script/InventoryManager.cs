using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("InventoryManager");
                    _instance = go.AddComponent<InventoryManager>();
                }
            }
            return _instance;
        }
    }

    public int money;
    private List<ClothingItem> clothingItems = new List<ClothingItem>();

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ClothingItem item)
    {
        clothingItems.Add(item);
        Debug.Log("Added item: " + item.itemName);
    }

    public void RemoveItem(ClothingItem item)
    {
        clothingItems.Remove(item);
        Debug.Log("Removed item: " + item.itemName);
    }

    public List<ClothingItem> GetClothingItems()
    {
        return clothingItems;
    }
}
