using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    public SaveDataManager dataManager;

    [Header("PANELS")]
    //public GameObject mainCanvas;
    public GameObject playerUpgradePanel;
    public GameObject WeaponsUpgradePanel;
    public GameObject linePlayer;
    public GameObject lineWeapons;

    //[SerializeField] private Text playerName;
    [Header("Text Infor Player")]
    [SerializeField] private Text txtHealth;
    [SerializeField] private Text txtEnery;
    [SerializeField] private Text txtSpeed;


    [Header("Text Button")]
    [SerializeField] private Text btnHealth;
    [SerializeField] private Text btnSpeed;
    [SerializeField] private Text btnEnery;

    // 
    private int coinHp;
    private int coinSpeed;
    private int coinEnery;
    // Start is called before the first frame update
    void Start()
    {
        Display();

    }

    private void LoadDataPlayer() {
        player = dataManager.playerdata.player;
        //coin = data.coin;
    }

    private void SaveDataPlayer()
    {
        dataManager.playerdata.player = this.player;
        dataManager.SaveData("player");
    }

    private void CoinToUpgrade()
    {
        // tính giá tiền cần để nâng cấp
        coinHp = Convert.ToInt32(player.health );
        coinSpeed = Convert.ToInt32(player.speed );
        coinEnery = Convert.ToInt32(player.enery );
    }

    public void Display()
    {
        LoadDataPlayer();
        CoinToUpgrade();
        txtHealth.text = "Heath: " + (player.health);
        txtSpeed.text = "Speed: " + (player.speed);
        txtEnery.text = "Enery: " + (player.enery);


        btnHealth.text = coinHp + "$";
        btnSpeed.text = coinSpeed + "$";
        btnEnery.text = coinEnery + "$";
    }

    public float ConvertFloat(float number)
    {
        float result = Convert.ToInt32(number * 10);
        result = Convert.ToSingle(result) / 10;
        return result;
    }

    public void UpgradeHp()
    {

        if (CoinManager.instance.GetCoin() >= coinHp)
        {
            player.health += player.health / 10;
            CoinManager.instance.MinusCoin(coinHp);
            SaveDataPlayer();
            Display();
        }
    }

    public void UpgradeSpeed()
    {
        if (CoinManager.instance.GetCoin() >= coinSpeed)
        {
            player.speed += player.speed / 10;
            CoinManager.instance.MinusCoin(coinSpeed);
            SaveDataPlayer() ;
            Display();
        }
    }
    public void UpgradeEnery()
    {
        if (CoinManager.instance.GetCoin() >= coinEnery)
        {
            player.enery += player.enery / 10;
            CoinManager.instance.MinusCoin(coinEnery);
            SaveDataPlayer();
            Display();
        }
    }

    void DisablePanels()
    {
        WeaponsUpgradePanel.SetActive(false);
        playerUpgradePanel.SetActive(false);

        linePlayer.SetActive(false);
        lineWeapons.SetActive(false);
    }

    public void DisplayPlayerPanel()
    {
        DisablePanels();
        playerUpgradePanel.SetActive(true);
        linePlayer.SetActive(true);
    }

    public void DisplayWeaponsPanel()
    {
        DisablePanels();
        WeaponsUpgradePanel.SetActive(true);
        lineWeapons.SetActive(true);
    }
}
