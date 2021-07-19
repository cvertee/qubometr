using System.Collections;
using UnityEngine;
using Zenject;

public class StoreManager : MonoBehaviour
{
    private AudioManager audioManager;
    private GameManager gameManager;
    private GameSettingsSO gameSettings;

    [Inject]
    public void Init(
        AudioManager audioManager,
        GameManager gameManager,
        GameSettingsSO gameSettings)
    {
        this.audioManager = audioManager;
        this.gameManager = gameManager;
        this.gameSettings = gameSettings;
    }
    
    public void TryBuyItem(ItemSO item, Player player)
    {
        var totalPrice = (int)(item.price * gameSettings.priceMultiplier);

        if (GameData.Data.coins < totalPrice)
        {
            Debug.Log("Can't buy this item due to high level of player's poverty");
            // TODO: display something
            return;
        }

        GameData.Data.coins -= totalPrice;
        GameData.Data.totalWastedCoins += totalPrice;

        gameManager.AddItemById(item.name, player);
        audioManager.PlaySound(AudioResource.StoreBuy);
        GameEvents.onDelayedActionRequested.Invoke(0.4f, () => audioManager.PlayClip(item.pickupSound));
    }
}