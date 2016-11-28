using UnityEngine;
using System.Collections;

public class HealthPlayer : MonoBehaviour {

    public static HealthPlayer instane;

    public int health = 2;

    [SerializeField]
    private GameObject player;

    private GamePlayManager gameManager;

    private CircleCollider2D boxPlayer;
    private SpriteRenderer spritePlayer;

    [SerializeField]
    private GameObject barrel;

    [HideInInspector]
    public bool isDead;

    public int countLife;
    public GameObject explosionPlayer;

    private string isNetwork;
    PhotonView m_PhotonView;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipGameOver;

    private string isMusic;

    private float timeAmor;
    public GameObject Amor;

    void Awake() {

        m_PhotonView = GetComponent<PhotonView>();

        spritePlayer = GetComponent<SpriteRenderer>();
        boxPlayer = GetComponent<CircleCollider2D>();

        MakeInstance();
    }

    void MakeInstance() {
        if (instane == null) {
            instane = this;
        }
    }

    // Use this for initialization
    void Start()
    {
 //       countLife = 3;
        countLife = PlayerPrefs.GetInt("Life");
        isNetwork = PlayerPrefs.GetString("isNetwork");

        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }

        timeAmor = 1.5f;
        Amor.SetActive(true);
    }

    void Update()
    {
        if(timeAmor > 0)
        {
            timeAmor -= Time.deltaTime;           
        }

        if (timeAmor <= 0)
        {
            Amor.SetActive(false);
            timeAmor = 0;
        }       
    }

    public void HealthUpdate(int damage)
    {
/*        if (PhotonNetwork.connected && m_PhotonView.isMine == false)
        {
            return;
        }
 */

        if (!player)
            return;
        if (health > 1)
        {
            health -= damage;
        }
        else
        {
            Dead();            
//            gameManager.GetComponent<GamePlayManager>().updateCountLife();
            DecreaseCountLife();

            if (countLife >= 0)
            {
                StartCoroutine("resSpawn", 0.7f);
            }
            else {
                //audioSource.PlayOneShot(clipGameOver);
            }

        }
    }

    IEnumerator resSpawn(float time) {
        yield return new WaitForSeconds(time);
        isDead = false;
        spritePlayer.sortingOrder = 2;
        barrel.GetComponent<SpriteRenderer>().sortingOrder =3;
        boxPlayer.enabled = true;

        gameObject.transform.position = new Vector3(28.3f, -4.01f, 0);      
        health = 1;
        timeAmor = 1.5f;
        Amor.SetActive(true);
    }

    public void Dead()
    {
        Instantiate(explosionPlayer, transform.position, Quaternion.identity);
        CameraShake.instance.Shake(0.002f, 0.02f);
        isDead = true;
        spritePlayer.sortingOrder = -1;
        barrel.GetComponent<SpriteRenderer>().sortingOrder = -1;
        boxPlayer.enabled = false;
    }

    private void DecreaseCountLife()
    {
/*        if (PhotonNetwork.connected && m_PhotonView.isMine == false)
        {
            return;
        }
 */
        countLife--;
        PlayerPrefs.SetInt("Life", countLife);
    }

    public void IncreaseCountLife()
    {
/*        if (PhotonNetwork.connected && m_PhotonView.isMine == false)
        {
            return;
        }
 */
        countLife++;
        if (countLife > 3)
        {
            countLife = 3;
        }
        PlayerPrefs.SetInt("Life", countLife);
    }

    public int getCountLife()
    {
        if (countLife < 0)
        {
            return 0;
        }
        return countLife;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "ItemAmor")
        {
            Amor.SetActive(true);
            timeAmor = 7f;
        }
    }

}
