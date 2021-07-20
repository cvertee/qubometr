using UnityEngine;

namespace Core
{
    public class GameObjectDestroyer : MonoBehaviour, IObjectDestroyer
    {
        public void CheckIfDestroyed(GameObject gameObject)
        {
            if (GameData.Data.destroyedItems.Contains(name))
            {
                Debug.Log($"Destroyable item {name} was saved as destroyed, destroying");
                Destroy(gameObject);
            }
        }
        
        public void DestroyAndSave(GameObject gameObject)
        {
            Destroy(gameObject);
            GameData.Data.destroyedItems.Add(name);
        }
    }
}