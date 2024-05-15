using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool isWin { get; private set; }
    public int coutEnemyDead { get; private set; }

   
    private void Awake()
    {
        if(instance == null || instance!=this) {
            Destroy(instance);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        isWin = false;
        coutEnemyDead = 0;
    }

    public void CountEnemy()
    {
        coutEnemyDead++;
        //Debug.Log(coutEnemyDead.ToString());
    }

    public void EndGame()
    {
        if (isWin)
        {
            CoinManager.instance.AddCoin(coutEnemyDead * 100);
            InventoryManager.Instance.SaveItems();
        }
        else
        {
            CoinManager.instance.AddCoin(coutEnemyDead * 5);
        }
        ScreenManager.Instance.DisplayEndGamePanel();

        StartCoroutine(PauseGame());
    }
    
    public void SetIsEnd(bool isWin)
    {
        this.isWin = isWin;
    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
}
