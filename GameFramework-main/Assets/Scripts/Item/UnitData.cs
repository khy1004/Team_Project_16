using UnityEngine;
using System;

public class UnitData
{
    public int id;
    public string UnitName;
    public string nameEng;
    public string description;
    public string itemTypeString;

    public int price;
    public int power;
    public ItemType itemType;

    public void InitalizeEnums()
    {
        if(Enum.TryParse(itemTypeString, out ItemType parsedType))
        {
            itemType = parsedType;
        }
        else
        {
            Debug.LogError($"아이템 '{UnitName}'에 유효하지 않은 아이템 타입이 있습니다. : {itemTypeString}");
            itemType = ItemType.None;
        }
    }
}
