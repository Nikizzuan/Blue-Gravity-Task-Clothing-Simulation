using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Text itemNameText;
    public Text itemPriceText;
    public Button button;

    private ClothingItem item;
    private Shopkeeper shopkeeper;

    public void Setup(ClothingItem item, Shopkeeper shopkeeper)
    {
        this.item = item;
        this.shopkeeper = shopkeeper;

        itemNameText.text = item.itemName;
        itemPriceText.text = item.price.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
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
    }
}
