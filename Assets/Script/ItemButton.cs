using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image itemPreviewImage;
    public TMP_Text itemNameText;
    public TMP_Text itemPriceText;
    public TMP_Text buttonText; // Reference to the button's text component
    public Button button;

    private ClothingItem item;
    private Shopkeeper shopkeeper;
    private InventoryManager inventoryManager;
    private PlayerController playerController;

    public void Setup(ClothingItem item, Shopkeeper shopkeeper, InventoryManager inventoryManager, PlayerController playerController)
    {
        this.item = item;
        this.shopkeeper = shopkeeper;
        this.inventoryManager = inventoryManager;
        this.playerController = playerController;

        itemPreviewImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemPriceText.text = item.price.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);

        // Set button text based on active UI and equipped status
        if (shopkeeper.shopUI.activeSelf)
        {
            buttonText.text = "Buy";
        }
        else if (shopkeeper.sellUI.activeSelf)
        {
            buttonText.text = "Sell";
        }
        else
        {
            buttonText.text = playerController.IsItemEquipped(item) ? "Equipped" : "Equip";
        }
    }

    private void OnButtonClick()
    {
        if (shopkeeper.shopUI.activeSelf)
        {
            shopkeeper.BuyItem(item);
        }
        else if (shopkeeper.sellUI.activeSelf)
        {
            shopkeeper.SellItem(item);
        }
        else
        {
            // Equip the item if it is in the player's inventory UI
            playerController.EquipClothingItem(item);
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        // Update button text for all item buttons
        ItemButton[] allItemButtons = FindObjectsOfType<ItemButton>();
        foreach (ItemButton itemButton in allItemButtons)
        {
            if (itemButton.item == item)
            {
                itemButton.buttonText.text = "Equipped";
            }
            else if (playerController.IsItemEquipped(itemButton.item))
            {
                itemButton.buttonText.text = "Equipped";
            }
            else
            {
                itemButton.buttonText.text = "Equip";
            }
        }
    }
}
