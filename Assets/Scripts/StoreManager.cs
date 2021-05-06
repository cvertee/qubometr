using Assets.Scripts.Game;
using Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class StoreManager : MonoBehaviour
    {
        public static void TryBuyItem(Item item, Player player)
        {
            if (GameData.Data.coins < item.price)
            {
                Debug.Log("Can't buy this item due to high level of player's poverty");
                // TODO: display something
                return;
            }

            GameData.Data.coins -= item.price;
            GameManager.Instance.AddItemById(item.id, player);
            AudioManager.Instance.PlaySound("cash");
            // TODO: save data on total coin waste
        }
    }
}