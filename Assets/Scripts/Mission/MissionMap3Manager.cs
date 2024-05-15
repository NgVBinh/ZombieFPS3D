using UnityEngine;

public class MissionMap3Manager : MonoBehaviour
{
    [SerializeField] private SaveDataManager dataManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!dataManager.mapdata.maps[2].isWon)
            {
                dataManager.mapdata.maps[2].isWon = true;
                dataManager.mapdata.maps[2].canOpen = true;
                dataManager.SaveData("map");
            }
            GameManager.instance.SetIsEnd(true);
            GameManager.instance.EndGame();
        }
    }
}
