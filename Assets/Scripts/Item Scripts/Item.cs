using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public static Item instance;

    public float DestroyTime;

    public bool isActiveStone;

    private GameObject stonePlayer;

    private SpriteRenderer spriteItem;
    private BoxCollider2D boxItem;

    public AudioSource audioSource;
    public AudioClip itemClip;
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

        Destroy(gameObject, DestroyTime);
        MakeInstance();

        spriteItem = GetComponent<SpriteRenderer>();
        boxItem = GetComponent<BoxCollider2D>();
    }

    void MakeInstance()
    {
        if(instance == null){
            instance = this;
        }
    }

    void Start()
    {
        isActiveStone = false;
        
    }

    void Update()
    {
//        stonePlayer = GameObject.FindGameObjectWithTag("New");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource.PlayOneShot(itemClip);
            boxItem.enabled = false;
            spriteItem.enabled = false;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("StonePlayer"))
            { 
                go.GetComponent<SpriteRenderer>().enabled = true;
                go.GetComponent<BoxCollider2D>().enabled = true;
            }
//            GameObject.FindGameObjectWithTag("StonePlayer").GetComponent<SpriteRenderer>().enabled = true;
//            stonePlayer.SetActive(true);
            isActiveStone = true;
            
        }
    }
    
}
