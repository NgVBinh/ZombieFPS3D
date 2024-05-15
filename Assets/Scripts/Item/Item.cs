using UnityEngine;

[CreateAssetMenu(fileName ="item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite image;
}
