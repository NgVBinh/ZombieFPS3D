using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] private SaveDataManager dataManager;
    private int coin;

    // display coin
    [SerializeField] private Text txtCoin;
    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(instance);
        }
        instance = this;
    }
    private void LoadCoin()
    {
        coin = dataManager.coindata.coin;
    }

    public int GetCoin()
    {
        return this.coin;
    }

    public void AddCoin(int coinAdd)
    {
        LoadCoin();
        this.coin += coinAdd;
        SaveCoin();
        if(txtCoin != null)
        DisplayCoin();
    }

    public void MinusCoin(int coinMinus)
    {
        LoadCoin();
        if (this.coin >= coinMinus) {
            this.coin -= coinMinus;
            SaveCoin();
            if (txtCoin != null) 
            DisplayCoin();
        }
        else
        {
            Debug.Log("Không đủ tiền nâng cấp");
        }

    }

    public void SaveCoin()
    {
        dataManager.coindata.coin= this.coin;
        dataManager.SaveData("coin");
    }

    public void DisplayCoin()
    {
        if (txtCoin == null) return;
        LoadCoin();
        //Debug.Log("Display coin"+ coin.ToString());
        txtCoin.text = coin.ToString() + "$";
    }

    private void OnEnable()
    {
        DisplayCoin();
    }
}
