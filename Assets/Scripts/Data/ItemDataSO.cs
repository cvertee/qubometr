using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemDataInfo
    {
        public string name; // maybe not that required? we'll probably see it in the future
        public ItemSO item;
    }
    
    // List of items' data (scriptable object) and their names that prevents resource loading
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item/Item data", order = 0)]
    public class ItemDataSO : ScriptableObject, IItemDatabase
    {
        [SerializeField] private List<ItemDataInfo> itemDataList;

        public ItemSO GetItemDataByName(string itemName) => itemDataList.FirstOrDefault(x => x.name == itemName)?.item;
    }
}