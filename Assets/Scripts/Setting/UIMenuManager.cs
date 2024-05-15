using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    private Animator CameraObject;

    // campaign button sub menu
    [Header("MENUS")]
    public GameObject mainMenu;
    public GameObject firstMenu;
    public GameObject exitMenu;
    public GameObject mapsPanel;
    public GameObject settingPanel;
    public GameObject upgradePanel;
    public GameObject instructionPanel;
    public GameObject canvCoin;

    [Header("PANELS")]
    public GameObject mainCanvas;
    public GameObject PanelControls;
    public GameObject PanelGame;


    // highlights in settings screen
    [Header("SETTINGS SCREEN")]
    public GameObject lineGame;
    public GameObject lineControls;


    [Header("SFX")]
    public AudioSource hoverSound;
    public AudioSource sliderSound;
    public AudioSource swooshSound;

    void Start()
    {
        Time.timeScale = 1.0f;
        CameraObject = transform.GetComponent<Animator>();

        InitialPanel();
    }

    public void InitialPanel()
    {
        firstMenu.SetActive(true);
        mainMenu.SetActive(true);
        mapsPanel.SetActive(false);
        instructionPanel.SetActive(false);
        upgradePanel.SetActive(false);
        settingPanel.SetActive(false);
        exitMenu.SetActive(false);
        canvCoin.SetActive(false);
    }

    public void ReturnMenu()
    {
        //if (settingPanel) settingPanel.SetActive(false);
        //if (upgradePanel) upgradePanel.SetActive(false);
        //exitMenu.SetActive(false);
        //firstMenu.SetActive(true);
        //mainMenu.SetActive(true);
        InitialPanel();
    }



    public void Position2()
    {
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
        ReturnMenu();
    }
    public void Position3()
    {
        if (!CameraObject.GetBool("Shop"))
        { 
            CameraObject.SetBool("Shop", true);
            canvCoin.SetActive(true);
        }
        else
        {
            CameraObject.SetBool("Shop", false);
            canvCoin.SetActive(false);
        }

    }
    void DisablePanels()
    {
        PanelControls.SetActive(false);
        PanelGame.SetActive(false);

        lineGame.SetActive(false);
        lineControls.SetActive(false);
    }

    public void GamePanel()
    {
        DisablePanels();
        PanelGame.SetActive(true);
        lineGame.SetActive(true);
    }

    public void ControlsPanel()
    {
        DisablePanels();
        PanelControls.SetActive(true);
        lineControls.SetActive(true);
    }

    public void DisplayUpgadeCanv()
    {
        if (mapsPanel) mapsPanel.SetActive(false);
        if (settingPanel) settingPanel.SetActive(false);
        if (instructionPanel) settingPanel.SetActive(false);
        canvCoin.SetActive(true);
        upgradePanel.SetActive(true);
    }
    public void DisplaySettingCanv()
    {
        if (mapsPanel) mapsPanel.SetActive(false);
        if (upgradePanel) upgradePanel.SetActive(false);
        if (instructionPanel) instructionPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void DisplayInstrucCanv()
    {
        if (mapsPanel) mapsPanel.SetActive(false);
        if (upgradePanel) upgradePanel.SetActive(false);
        if (settingPanel) settingPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        sliderSound.Play();
    }

    public void PlaySwoosh()
    {
        swooshSound.Play();
    }

    // Are You Sure - Quit Panel Pop Up
    public void DisplayExisPanel()
    {
        if (mapsPanel) mapsPanel.SetActive(false);
        if (exitMenu) exitMenu.SetActive(false);
        exitMenu.SetActive(true);
    }

    public void DisplayMapsPanel()
    {
        if (exitMenu) exitMenu.SetActive(false);
        if (mapsPanel) mapsPanel.SetActive(false);
        mapsPanel.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }

}