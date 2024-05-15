using UnityEngine;
using UnityEngine.UI;

public class MissionMap1Manager : MonoBehaviour
{
    public float timeMission;
    private float survivorTimer;
    private bool isMissionComplete;

    [SerializeField] private Text timerTxt;
    [SerializeField] private SaveDataManager dataManager;

    private void Start()
    {
        isMissionComplete = false;
        survivorTimer = timeMission;
    }
    private void Update()
    {
        Mission1();
    }
    public void Mission1()
    {
        
        survivorTimer -= Time.deltaTime;
        timerTxt.text = ((int)survivorTimer).ToString();
        if (survivorTimer <= 0 && !isMissionComplete)
        {
            if (!dataManager.mapdata.maps[0].isWon)
            {
                dataManager.mapdata.maps[0].isWon = true;
                dataManager.mapdata.maps[1].canOpen = true;
                dataManager.SaveData("map");
                Debug.Log(dataManager.mapdata.maps[1].canOpen);
            }

            isMissionComplete = true;
            GameManager.instance.SetIsEnd(true);
            GameManager.instance.EndGame();
        }

        
    }
}
