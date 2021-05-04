using Assets.Scripts.Core;
using Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PickableItem : MonoBehaviour, IInteractable
    {

        public string itemId;

        private void Start()
        {
            if (string.IsNullOrEmpty(itemId))
            {
                Debug.LogWarning($"No itemId for pickable item! | {transform.position} | {name}");
                Destroy(gameObject); // TODO: use placeholder?
            }

            var item = Resources.Load<Item>($"Prefabs/Items/{itemId}");
            if (item == null)
            {
                Debug.LogWarning($"Pickable item with {itemId} id does not exist or it's a bug");
                Destroy(gameObject);
            }

            GetComponent<SpriteRenderer>().sprite = item.icon;
        }
        public void Interact()
        {
            GameManager.Instance.AddItemById(itemId, FindObjectOfType<Player>()); // TODO: replace find object with somethign else
            // TODO: play pickup sound
            Destroy(gameObject);
        }

        public void StopInteract()
        {
            throw new System.NotImplementedException();
        }
    }
}