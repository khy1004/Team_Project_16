using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string UnitName;    
    public string nameEng;
    public string description;


    public ItemType itemType;
    public int price;
    public int power;


    public override string ToString()
    {
       return $"[{id}] {UnitName} - 가격 : {price} 골드, 속성 : {power}";
    }

    public string DisplayName
    {
        get { return string.IsNullOrEmpty(nameEng) ? UnitName : nameEng; }
    }
}
