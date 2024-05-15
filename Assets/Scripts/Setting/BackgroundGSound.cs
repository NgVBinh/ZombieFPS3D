using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicBGVolume");
    }

    public void UpdateBGVolume()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicBGVolume");
    }
}
