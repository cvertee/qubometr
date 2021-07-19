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
    private GameSettingsSO gameSettings;
    
    [Inject]
    public void Init(
        StoreManager storeManager,
        GameSettingsSO gameSettings)
    {
        this.storeManager = storeManager;
        this.gameSettings = gameSettings;
    }

    public void Initialize(ItemSO item)
    {
        itemUiIcon.sprite = item.icon;
        itemNameText.text = LocalizationUtil.IdToLocalized(item.displayName);
        itemPriceText.text = (item.price * gameSettings.priceMultiplier).ToString();

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