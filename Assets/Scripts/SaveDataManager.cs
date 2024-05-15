using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public PlayerData playerdata = new PlayerData();
    private string fileNamePlayerData = "DataPlayer.json";
    private string filePathPlayerData;

    public CoinData coindata = new CoinData();
    private string fileNameCoinData = "DataCoin.json";
    private string filePathCoinData;

    public ItemsData itemsdata = new ItemsData();
    private string fileNameItemsData = "ItemsCoin.json";
    private string filePathItemsData;

    public MapData mapdata = new MapData();
    private string fileNameMapData = "DataMap.json";
    private string filePathMapData;

    private void Awake()
    {
        filePathPlayerData = Path.Combine(Application.persistentDataPath, fileNamePlayerData);
        filePathCoinData = Path.Combine(Application.persistentDataPath, fileNameCoinData);
        filePathItemsData = Path.Combine(Application.persistentDataPath, fileNameItemsData);
        filePathMapData = Path.Combine(Application.persistentDataPath, fileNameMapData);

        SetDataInitial();
        LoadData("player");
        LoadData("coin");
        LoadData("items");
        LoadData("map");
        //Debug.Log(playerdata.player.name);
    }

    private void SetDataInitial()
    {
        if (!File.Exists(filePathPlayerData))
        {
            Player playerInit = new Player(00, "player00", 10, 100, 3.5f);
            playerdata.player = playerInit;
            SaveData("player");
        }

        if (!File.Exists(filePathCoinData))
        {
            coindata.coin = 1000;
            SaveData("coin");
        }

        if (!File.Exists(filePathItemsData))
        {
            SaveData("items");
        }

        if (!File.Exists(filePathMapData))
        {
            Map map1 = new Map(0,"Thành phố",false,true);
            mapdata.maps.Add(map1);
            Map map2 = new Map(1, "Lâu đài", false, false);
            mapdata.maps.Add(map2);
            Map map3 = new Map(2, "Kho quân sự", false, false);
            mapdata.maps.Add(map3);
           
            SaveData("map");
        }
    }

    public void SaveData(string name)
    {
        switch (name)
        {
            case "player":
                string dataJsonPlayer = JsonUtility.ToJson(playerdata);
                File.WriteAllText(filePathPlayerData, dataJsonPlayer);
                Debug.Log("save player data to " + filePathPlayerData);
                break;
            case "coin":
                string dataJsonCoin = JsonUtility.ToJson(coindata);
                File.WriteAllText(filePathCoinData, dataJsonCoin);
                Debug.Log("save coin data to " + filePathCoinData);
                break;
            case "items":
                string dataJsonItems = JsonUtility.ToJson(itemsdata);
                File.WriteAllText(filePathItemsData, dataJsonItems);
                Debug.Log("save items data to " + filePathItemsData);
                break;
            case "map":
                string dataJsonMap = JsonUtility.ToJson(mapdata);
                File.WriteAllText(filePathMapData, dataJsonMap);
                Debug.Log("save map data to " + filePathMapData);
                break;
        }

    }

    public void LoadData(string name)
    {
        switch (name)
        {
            case "player":
                if (File.Exists(filePathPlayerData))
                {
                    string dataJsonPlayer = File.ReadAllText(filePathPlayerData);
                    playerdata = JsonUtility.FromJson<PlayerData>(dataJsonPlayer);
                }
                else
                {
                    Debug.LogError("File player not found: " + filePathPlayerData);
                }
                break;
            case "coin":
                if (File.Exists(filePathCoinData))
                {
                    string dataJsonCoin = File.ReadAllText(filePathCoinData);
                    coindata = JsonUtility.FromJson<CoinData>(dataJsonCoin);
                }
                else
                {
                    Debug.LogError("File coin not found: " + filePathCoinData);
                }
                break;
            case "items":
                if (File.Exists(filePathItemsData))
                {
                    string dataJsonItems = File.ReadAllText(filePathItemsData);
                    itemsdata = JsonUtility.FromJson<ItemsData>(dataJsonItems);
                }
                else
                {
                    Debug.LogError("File items not found: " + filePathItemsData);
                }
                break;

            case "map":
                if (File.Exists(filePathMapData))
                {
                    string dataJsonMap = File.ReadAllText(filePathMapData);
                    mapdata = JsonUtility.FromJson<MapData>(dataJsonMap);
                }
                else
                {
                    Debug.LogError("File map not found: " + filePathMapData);
                }
                break;
        }


    }

}
[Serializable]
public class CoinData
{
    public int coin;
}
[Serializable]
public class ItemsData
{
    public List<Item> items;
}

[Serializable]
public class PlayerData
{
    public Player player;
}

[Serializable]
public class Player
{

    public int id;
    public string name;
    public float health;
    public float enery;
    public float speed;

    public Player() { }
    public Player(int id, string name, float health, float enery, float speed)
    {
        this.id = id;
        this.name = name;
        this.health = health;
        this.enery = enery;
        this.speed = speed;
    }
}

[Serializable]
public class MapData
{
    public List<Map> maps;
}

[Serializable]
public class Map
{

    public int id;
    public string name;
    public bool isWon;
    public bool canOpen;

    public Map() { }
    public Map(int id, string name, bool isWon, bool canOpen)
    {
        this.id = id;
        this.name = name;
        this.isWon = isWon;
        this.canOpen = canOpen;
    }
}
