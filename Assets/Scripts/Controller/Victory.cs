using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

    const string VICTORY = "Victory";

    [HideInInspector]
    public bool isVictory;

    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipVictory;

    private string isMusic;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
        isVictory = false;

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
        if (target.tag == "bulletplayer" || target.tag == "Rocket") {
            isVictory = true;
            anim.Play(VICTORY);
            audioSource.PlayOneShot(clipVictory);
        }
    }

}
