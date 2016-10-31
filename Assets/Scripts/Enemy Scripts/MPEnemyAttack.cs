using UnityEngine;
using System.Collections;

public class MPEnemyAttack : MonoBehaviour {
    public GameObject Bullet;
    public GameObject bulletPos;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipShoot;

    private string isMusic;
    private PhotonView photonView;
    private SpriteRenderer sprite;
    private BoxCollider2D box2D;

    private bool isDead;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        sprite = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        isDead = false;

        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
        //        StartCoroutine(Attack());
        photonView.RPC("Attack", PhotonTargets.All); 
    }

    [PunRPC]
    void Shoot()
    {
        StartCoroutine(Attack());
    }

    [PunRPC]
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        var bullet = (GameObject)Instantiate(Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        var bulletBody = (Rigidbody2D)bullet.GetComponentInChildren(typeof(Rigidbody2D));
        //            bulletBody.AddForce( bulletPos.transform.up * speedBullet, ForceMode2D.Impulse);
        bulletBody.velocity = bulletPos.transform.up * 10;

        if (transform.localScale.y < 0)
        {
            bulletBody.velocity = bulletPos.transform.up * -10;
        }

        audioSource.PlayOneShot(clipShoot);
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (isDead)
        {
            isDead = false;
            photonView.RPC("SpawnEnemy", PhotonTargets.All);

        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "bulletplayer")
        {
            photonView.RPC("Destroy", PhotonTargets.All);
           
        }
        
    }

    [PunRPC]
    void Destroy()
    {       
//        Destroy(gameObject);
        sprite.enabled = false;
        box2D.enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        isDead = true;
    }

    [PunRPC]
    void SpawnEnemy()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        sprite.enabled = true;
        box2D.enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
