using UnityEngine;
using System.Collections;

public class ChangeBulletConfig : MonoBehaviour {

    public float destroyTime;
    public BulletConfig config;

    public AudioSource audioSource;
    public AudioClip itemClip;

    private SpriteRenderer spriteItem;
    private BoxCollider2D boxItem;

    private string isMusic;

    void Awake()
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

        spriteItem = GetComponent<SpriteRenderer>();
        boxItem = GetComponent<BoxCollider2D>();

        if (config == null)
        {
            Debug.LogWarning("ChangeBulletCOnfig, missing config !!!", gameObject);
            Destroy(this);
        }

        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            audioSource.PlayOneShot(itemClip);
            target.gameObject.GetComponent<PlayerController>().ChangeBulletConfig(config);
            boxItem.enabled = false;
            spriteItem.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
