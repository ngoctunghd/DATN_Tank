using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float DestroyTime;

    private string isMusic;

    void Awake()
    {
        Destroy(gameObject, DestroyTime);

        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GetComponent<AudioSource>().mute = false;
            
        }
    }

}
