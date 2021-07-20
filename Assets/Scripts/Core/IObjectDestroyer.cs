using UnityEngine;

namespace Core
{
    public interface IObjectDestroyer
    {
        void CheckIfDestroyed(GameObject gameObject);
        void DestroyAndSave(GameObject gameObject);
    }
}