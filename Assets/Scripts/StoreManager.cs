using System.Collections;
using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    public void TryBuyItem(Item item, Player player)
    {
        var totalPrice = (int)(item.price * GameSettings.GlobalPriceMultiplier);

        if (GameData.Data.coins < totalPrice)
        {
            Debug.Log("Can't buy this item due to high level of player's poverty");
            // TODO: display something
            return;
        }

        GameData.Data.coins -= totalPrice;
        GameData.Data.totalWastedCoins += totalPrice;

        GameManager.Instance.AddItemById(item.id, player);
        Sound.Play(AudioResource.StoreBuy);
        GameEvents.onDelayedActionRequested.Invoke(0.4f, () => Sound.Play(item.pickupSound));
    }
}