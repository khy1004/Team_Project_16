using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBaseSO", menuName = "ScriptableObjects/ItemDataBaseSO")]
public class ItemDataBaseSO : ScriptableObject
{
    public List<ItemSO> items = new List<ItemSO>();

    private Dictionary<int, ItemSO> itemById;
    private Dictionary<string, ItemSO> itemByName;

    public void Initialze()
    {
        itemById = new Dictionary<int, ItemSO>();
        itemByName = new Dictionary<string, ItemSO>();
    
        foreach(var item in items)
        {
            itemById[item.id] = item;
            itemByName[item.UnitName] = item;
        }

    }

    public ItemSO GetItemById(int id)
    {
        if (itemById == null)
        {
            Initialze();
        }

        if (itemById.TryGetValue(id, out ItemSO item))
         return item;

        return null;

    }

    public ItemSO GetItemByName(string name)
    {
        if (itemByName == null)
        {
            Initialze();
        }
        if (itemByName.TryGetValue(name, out ItemSO item))
         return item;

        return null;
    }

    public List<ItemSO> GetAllItems(ItemType type)
    {
        return items.FindAll(item => item.itemType == type);
    }

}
