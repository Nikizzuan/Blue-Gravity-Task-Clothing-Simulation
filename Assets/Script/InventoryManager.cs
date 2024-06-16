using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public TMP_Text moneyText;
    public List<ClothingItem> clothingItems = new List<ClothingItem>();

    public PlayerEquipment _playerEquipment;

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

        moneyText.text = money.ToString();
        foreach (var item in clothingItems)
        {
            _playerEquipment.EquipItem(item);
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

    public void UpdateMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
}
