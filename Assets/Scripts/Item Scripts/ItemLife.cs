using UnityEngine;
using System.Collections;

public class ItemLife : MonoBehaviour {
    public static ItemLife instance;

    public float DestroyTime;

    private SpriteRenderer spriteItem;
    private BoxCollider2D boxItem;

    private HealthPlayer Player;

    public AudioSource audioSource;
    public AudioClip itemClip;
    private string isMusic;


    void Awake()
    {
        Destroy(gameObject, DestroyTime);
        MakeInstance();
        spriteItem = GetComponent<SpriteRenderer>();
        boxItem = GetComponent<BoxCollider2D>();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();

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

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource.PlayOneShot(itemClip);
            boxItem.enabled = false;
            spriteItem.enabled = false;
            Player.IncreaseCountLife();
        }
    }
}
