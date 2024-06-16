using UnityEngine;
using System.Collections.Generic;

public class Shopkeeper : MonoBehaviour
{
    public GameObject interactionUI; // UI with Buy/Sell options
    public GameObject shopUI; // UI for buying items
    public GameObject sellUI; // UI for selling items
    public Transform shopItemsParent;
    public Transform playerItemsParent;
    public GameObject itemPrefab;
    public InventoryManager inventoryManager;
    public List<ClothingItem> itemsForSale;


    private bool isPlayerNearby = false;

 

    void Update()
    {   
        
        if (!interactionUI.activeSelf && !shopUI.activeSelf && !sellUI.activeSelf && isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            interactionUI.SetActive(true);
        }
      

    }

    public void Buy()
    {
        if (interactionUI != null) interactionUI.SetActive(false);
        if (shopUI != null) shopUI.SetActive(true);
        PopulateShopItems();
    }

    public void Sell()
    {
        if (interactionUI != null) interactionUI.SetActive(false);
        if (sellUI != null) sellUI.SetActive(true);
        PopulatePlayerItems();
    }

    public void CloseUI()
    {
        if (interactionUI != null) interactionUI.SetActive(false);
        if (shopUI != null) shopUI.SetActive(false);
        if (sellUI != null) sellUI.SetActive(false);
    }

    private void PopulateShopItems()
    {
        foreach (Transform child in shopItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ClothingItem item in itemsForSale)
        {
            GameObject itemGO = Instantiate(itemPrefab, shopItemsParent);
            // Setup itemGO with item data
            itemGO.GetComponent<ItemButton>().Setup(item, this, inventoryManager, GameManager.Instance.playerController);
        }
    }

    private void PopulatePlayerItems()
    {
        foreach (Transform child in playerItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ClothingItem item in inventoryManager.GetClothingItems())
        {
            GameObject itemGO = Instantiate(itemPrefab, playerItemsParent);
            // Setup itemGO with item data
            itemGO.GetComponent<ItemButton>().Setup(item, this, inventoryManager, GameManager.Instance.playerController);
        }
    }

    public void BuyItem(ClothingItem item)
    {
        if (inventoryManager.money >= item.price)
        {
            inventoryManager.AddItem(item);
            inventoryManager.UpdateMoney(-item.price);
            itemsForSale.Remove(item);
            PopulateShopItems();
        }
        else
        {
            // Show insufficient balance UI
            Debug.Log("Insufficient balance");
        }
    }

   public void SellItem(ClothingItem item)
    {
        if (GameManager.Instance.playerController.IsItemEquipped(item))
        {
             GameManager.Instance._playerEquipment.UnEquipItem(item); 
        }

        inventoryManager.RemoveItem(item);
        inventoryManager.UpdateMoney(item.price);
        itemsForSale.Add(item);
        PopulatePlayerItems();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the shopkeeper");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the shopkeeper");
            CloseUI();
        }
    }
}
