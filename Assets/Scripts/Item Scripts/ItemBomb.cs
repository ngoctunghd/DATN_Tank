using UnityEngine;
using System.Collections;

public class ItemBomb : MonoBehaviour {

    public static ItemBomb instance;

    public float DestroyTime;

    private SpriteRenderer spriteItem;
    private BoxCollider2D boxItem;

    public GameObject explosion;
    public AudioSource audioSource;
    public AudioClip itemClip;
    private string isMusic;

    void Awake()
    {
        Destroy(gameObject, DestroyTime);
        MakeInstance();
        spriteItem = GetComponent<SpriteRenderer>();
        boxItem = GetComponent<BoxCollider2D>();

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

            Time.timeScale = 0.8f;
            StartCoroutine(resetTimeScale());
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("enemy"))
            {
                Destroy(gameObject);
                              
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();
                SpawnManager.instance.DecreaseCurrentEnemy();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Boss"))
            {
                Destroy(gameObject);
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();
                SpawnManager.instance.DecreaseCurrentEnemy();
            }

        }
    }

    IEnumerator resetTimeScale()
    {
        yield return new WaitForSeconds(.2f);
        Time.timeScale = 1f;        
    }


}
