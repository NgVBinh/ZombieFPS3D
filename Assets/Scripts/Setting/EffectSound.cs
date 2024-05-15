using UnityEngine;

public class EffectSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicEffectsVolume");
    }

    public void UpdateEffectVolume()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicEffectsVolume");
    }
}
