using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private string isMusic;

    // Use this for initialization
    void Start()
    {
        isMusic = PlayerPrefs.GetString("Music");
        
        if (isMusic == "Off")
        {
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }

    }
}
