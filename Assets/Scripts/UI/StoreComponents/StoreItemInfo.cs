using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoreItemInfo : MonoBehaviour
{
    public Image itemUiIcon;
    public StoreBuyButton buyButton;
    public Text itemNameText;
    public Text itemPriceText;

    private StoreManager storeManager;
    
    [Inject]
    public void Init(StoreManager storeManager)
    {
        this.storeManager = storeManager;
    }

    public void Initialize(ItemSO item)
    {
        itemUiIcon.sprite = item.icon;
        itemNameText.text = LocalizationUtil.IdToLocalized(item.displayName);
        itemPriceText.text = (item.price * GameSettings.GlobalPriceMultiplier).ToString();

        buyButton.onClick.AddListener(() =>
        {
            var player = FindObjectOfType<Player>();
            storeManager.TryBuyItem(item, player);
        });
    }

    public class Factory : PlaceholderFactory<StoreManager, StoreItemInfo>
    {
        
    }
}