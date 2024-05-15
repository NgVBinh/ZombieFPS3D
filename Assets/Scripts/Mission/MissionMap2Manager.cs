using UnityEngine;
using UnityEngine.UI;
public class MissionMap2Manager : MonoBehaviour
{
    [SerializeField] private Text countSoupTxt;
    private int countSoupCurrent;
    public int coutSoupMission;
    private bool isMissionComplete;

    public Item itemMission;

    [SerializeField] private SaveDataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        countSoupCurrent = 0;
        isMissionComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMission();
    }

    public void CheckMission()
    {
        int count = 0;
        foreach(var item in InventoryManager.Instance.GetItems())
        {
            if(item.name == itemMission.name)
            {
                count++;
            }
        }
        countSoupCurrent = count;
        countSoupTxt.text = countSoupCurrent.ToString();
        if (countSoupCurrent >= coutSoupMission &&!isMissionComplete)
        {
            if (!dataManager.mapdata.maps[1].isWon)
            {
                dataManager.mapdata.maps[1].isWon = true;
                dataManager.mapdata.maps[2].canOpen = true;
                dataManager.SaveData("map");
            }
            
            isMissionComplete = true;
            GameManager.instance.SetIsEnd(true);
            GameManager.instance.EndGame();
        }
    }
}
