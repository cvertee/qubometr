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
        itemNameText.text = LocalizationUtil.IdToLocalized(item.displayName);
        itemPriceText.text = (item.price * GameSettings.GlobalPriceMultiplier).ToString();

        buyButton.onClick.AddListener(() =>
        {
            var player = FindObjectOfType<Player>();
            StoreManager.Instance.TryBuyItem(item, player);
        });
    }
}