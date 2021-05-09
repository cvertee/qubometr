using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemInfo : MonoBehaviour
{
    public Image itemUiIcon;
    public StoreBuyButton buyButton;
    public Text itemNameText;
    public Text itemPriceText;

    public void Initialize(Item item)
    {
        itemUiIcon.sprite = item.icon;
        itemNameText.text = item.name; // TODO: use anything else
        itemPriceText.text = item.price.ToString();

        buyButton.onClick.AddListener(() =>
        {
            var player = FindObjectOfType<Player>();
            StoreManager.Instance.TryBuyItem(item, player);
        });
    }
}