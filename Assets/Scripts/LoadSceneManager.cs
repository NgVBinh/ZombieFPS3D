using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public GameObject mainCanvas;
    [Header("LOADING SCREEN")]
    public bool waitForInput = true;
    public GameObject loadingMenu;
    public Slider loadingBar;
    public TMP_Text loadPromptText;
    public KeyCode userPromptKey;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.value = 0;
    }

     public void ReplayGame()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string scene)
    {
        if (scene != "")
        {
            mainCanvas.SetActive(false);
            loadingMenu.SetActive(true);
            StartCoroutine(LoadAsynchronously(scene));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    { // scene name is just the name of the current scene being loaded

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Thiết lập transition None
        asyncLoad.allowSceneActivation = false;

        // Load scene và cập nhật giá trị của Slider trong quá trình này
        while (!asyncLoad.isDone)
        {
            // Tính toán tiến độ load
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9f là tiến trình của scene trước khi được activate

            // Cập nhật giá trị của Slider
            loadingBar.value = progress;

            // Nếu tiến độ load đạt 100% và không có transition nào thì activate scene
            if (progress >= 1f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
