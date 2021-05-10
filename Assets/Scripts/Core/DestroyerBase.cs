using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBase : MonoBehaviour
{
    private void Awake()
    {
        if (GameData.Data.destroyedItems.Contains(name))
        {
            Debug.Log($"Destroyable item {name} was saved as destroyed, destroying");
            Destroy(gameObject);
        }
    }
    
    protected void DestroySave()
    {
        GameData.Data.destroyedItems.Add(name);
        Destroy(gameObject);
    }
}
