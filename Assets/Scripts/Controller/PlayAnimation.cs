using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

    public static PlayAnimation instance;
    const string GAMEOVER = "GameOver";

    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipGameOver;

    private string isMusic;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "bulletplayer" || target.tag == "Rocket" || target.tag == "bulletenemy")
        {
            anim.Play(GAMEOVER);
            audioSource.PlayOneShot(clipGameOver);
        }
    }
}
