using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetiing : MonoBehaviour
{
    [Header("PANELS")]
    //public GameObject mainCanvas;
    public GameObject PanelGame;
    public GameObject PanelControls;

    public GameObject lineGame;
    public GameObject lineControls;
    [Header("CONTROLS SETTINGS")]
    // sliders
    public GameObject musicBGSlider;
    private float musicBGValue = 0.0f;
    public GameObject musicEffectsSlider;
    private float musicEffectsValue = 0.0f;

    // slider mouse
    public GameObject mouseSmoothSlider;
    private float sliderValueSmoothing = 0.0f;

    //
    public BackgroundGSound[] backgroundSounds;
    public EffectSound[] effectSounds;
    // Start is called before the first frame update
    void Start()
    {
        musicBGSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicBGVolume");
        musicEffectsSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicEffectsVolume");
        mouseSmoothSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MouseSmoothing");

        backgroundSounds = FindObjectsOfType<BackgroundGSound>();
        effectSounds = FindObjectsOfType<EffectSound>();
    }

    // Update is called once per frame
    void Update()
    {
        musicBGValue = musicBGSlider.GetComponent<Slider>().value;
        musicEffectsValue = musicEffectsSlider.GetComponent<Slider>().value;
        sliderValueSmoothing = mouseSmoothSlider.GetComponent<Slider>().value;
    }

    public void MusicBGSlider()
    {
        PlayerPrefs.SetFloat("MusicBGVolume", musicBGValue);
        PlayerPrefs.SetFloat("MusicBGVolume", musicBGSlider.GetComponent<Slider>().value);
    }

    public void MusicEffectsSlider()
    {
        PlayerPrefs.SetFloat("MusicEffectsVolume", musicEffectsValue);
        PlayerPrefs.SetFloat("MusicEffectsVolume", musicEffectsSlider.GetComponent<Slider>().value);
    }

    public void SensitivitySmoothing()
    {
        PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
        Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
    }

    public void UpdateSoundBackgrounds()
    {
        foreach(BackgroundGSound bgSound in backgroundSounds)
        {
            bgSound.UpdateBGVolume();
        }
    }

    public void UpdateSoundEffects()
    {
        foreach (EffectSound effSound in effectSounds)
        {
            effSound.UpdateEffectVolume();
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
}
