using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private SaveDataManager dataManager;
    private List<Item> items = new List<Item>();// lấy từ data
    
    // disp
    public Transform holderItemUI;
    public GameObject itemUIPrefab;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        dataManager.LoadData("items");
        items = dataManager.itemsdata.items;
        if (items.Count > 0)
        {
            DisplayItemInventory();
        }

    }

    public void AddItem(Item item)
    {
        items.Add(item);
        DisplayItemInventory();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        DisplayItemInventory();
    }

    public void DisplayItemInventory()// hien thi trong inventory ui
    {
        // xoa cac item co truoc
        foreach (Transform item in holderItemUI)
        {
            Destroy(item.gameObject);
        }
        // hien thi cac item
        foreach (Item item in items)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, holderItemUI);
            itemUI.GetComponent<ItemController>().SetItem(item);
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            itemUI.transform.Find("ItemImage").GetComponent<Image>().sprite = item.image;
        }
    }

    public Item GetItemInInventory(Item item)
    {
        foreach (Item tempItem in items)
        {
            if (tempItem == item)
                return tempItem;
        }
        return null;
    }

    public void SaveItems()
    {
        dataManager.itemsdata.items = items;
        dataManager.SaveData("items");
    }

    public List<Item> GetItems()
    {
        return this.items;
    }
}
