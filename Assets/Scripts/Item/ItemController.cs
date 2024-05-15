using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return this.item;
    }

    public void AddItem()
    {
        InventoryManager.Instance.AddItem(this.item);
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(this.item);

        InventoryControllerInGame inventoryController = FindObjectOfType<InventoryControllerInGame>();
        if (inventoryController != null)
        {
            inventoryController.PutOutOfItem(this.item);
        }
    }

    public void SaveItems()
    {
        InventoryManager.Instance.SaveItems();
    }

    public void UseItem()
    {
        switch (this.item.id)
        {
            case 0:
                PlayerManager.instance.ChangeHealthValue(this.item.value);
                InventoryManager.Instance.RemoveItem(this.item);
                break;
            case 1:
                PlayerManager.instance.ChangeEneryValue(this.item.value);
                InventoryManager.Instance.RemoveItem(this.item);
                break;
            case 2:
                PlayerManager.instance.ChangeHealthValue(this.item.value);
                PlayerManager.instance.ChangeEneryValue(this.item.value);
                InventoryManager.Instance.RemoveItem(this.item);
                break;
            case 3:
                FindObjectOfType<ShootController>().Reload();
                break;
        }
    }

    public void NotificationInforItem()
    {
        switch (this.item.id)
        {
            case 0:
                ScreenManager.Instance.NotificationInGame("Hồi phục " + item.value + " máu", 2f);
                break;
            case 1:
                ScreenManager.Instance.NotificationInGame("Hồi phục " + item.value + " năng lượng", 2f);
                break;
            case 2:
                ScreenManager.Instance.NotificationInGame("Hồi phục " + item.value + " máu, năng lượng", 2f);
                break;
            case 3:
                ScreenManager.Instance.NotificationInGame("Băng đạn", 2f);
                break;
            case 4:
                ScreenManager.Instance.NotificationInGame("Lựu đạn", 2f);
                break;
        }
    }
}
