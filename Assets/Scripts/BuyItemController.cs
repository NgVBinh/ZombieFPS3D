using UnityEngine;
using UnityEngine.UI;

public class BuyItemController : MonoBehaviour
{
    private Button buttonBuyItem;

    void Start()
    {
        buttonBuyItem = GetComponent<Button>();

        buttonBuyItem.onClick.AddListener(()=>BuyItem());
    }

    private void BuyItem()
    {
        ItemController itemController = GetComponentInParent<ItemController>();
        // trừ tiền
        itemController.AddItem();
        CoinManager.instance.MinusCoin(itemController.GetItem().value);
        //CoinManager.instance.DisplayCoin();
        InventoryManager.Instance.SaveItems();
        
    }


}
