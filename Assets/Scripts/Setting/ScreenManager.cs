using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    [Header("Panel")]
    [SerializeField] private GameObject ingamePanel;
    [SerializeField] private GameObject pausegamePanel;
    [SerializeField] private GameObject endgamePanel;
    [SerializeField] private GameObject endgameWinPanel;
    [SerializeField] private GameObject endgameLosePanel;

    [Header("Notification")]
    //Notification text
    [SerializeField] private Text txtNotification;

    [Header("Infor Player")]
    //infor player in game
    [SerializeField] private Image healthImageFiller;
    [SerializeField] private Image energyImageFiller;

    [Header("Infor EndGame")]
    //infor end game
    [SerializeField] private Text rewardText;
    [SerializeField] private Text countEnemyText;

    private ShootController shootController;
    private enum ScreenState
    {
        INGAME,
        PAUSEGAME,
        ENDGAME
    }
    private GameObject currentScreen;

    private void Awake()
    {
        if (Instance != null || Instance != this) Destroy(Instance);

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentScreen = ingamePanel;
        txtNotification.enabled=false;
       // shootController = FindObjectOfType<ShootController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayPauseGamePanel();
        }
    }

    private void ChangeScreen(ScreenState state)
    {
        currentScreen.SetActive(false);
        switch (state)
        {
            case ScreenState.INGAME:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                currentScreen = ingamePanel;

                PlayerManager.instance.setCanChangeWeapon(true);
                shootController = FindObjectOfType<ShootController>();
                shootController.SetCanShoot(true);
                break;
            case ScreenState.PAUSEGAME:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                currentScreen = pausegamePanel;

                PlayerManager.instance.setCanChangeWeapon(false);
                shootController = FindObjectOfType<ShootController>();
                shootController.SetCanShoot(false);
                break;
            case ScreenState.ENDGAME:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (GameManager.instance.isWin)
                {
                    endgameWinPanel.SetActive(true);
                    endgameLosePanel.SetActive(false);
                }
                else
                {
                    endgameWinPanel.SetActive(false);
                    endgameLosePanel.SetActive(true);
                }
                DisplayReward();
                currentScreen = endgamePanel;
                break;
        }
        currentScreen.SetActive(true);
    }

    public void DisplayPauseGamePanel()
    {
        ChangeScreen(ScreenState.PAUSEGAME);
    }
    public void DisplayInGamePanel()
    {
        ChangeScreen(ScreenState.INGAME);
    }
    public void DisplayEndGamePanel()
    {
        ChangeScreen(ScreenState.ENDGAME);
    }

    public void DisplayPlayerInfor()
    {
        healthImageFiller.fillAmount = PlayerManager.instance.currentHealth / PlayerManager.instance.maxHealth;

        energyImageFiller.fillAmount = PlayerManager.instance.currentEnergy / PlayerManager.instance.maxEnergy;
    }

    public void DisplayReward()
    {
        int countEnemyDead = GameManager.instance.coutEnemyDead;
        if (GameManager.instance.isWin)
        {
            rewardText.text = (countEnemyDead * 100).ToString();
        }
        else
        {
            rewardText.text = (countEnemyDead * 5).ToString();
        }
        countEnemyText.text = countEnemyDead.ToString();

    }

    public void NotificationInGame(string notification, float time)
    {
        txtNotification.enabled = true;
        txtNotification.text = notification;
        StartCoroutine(NotificationTimer(time));
    }

    IEnumerator NotificationTimer(float time)
    {
        yield return new WaitForSeconds(time);
        txtNotification.enabled = false;
    }
}
