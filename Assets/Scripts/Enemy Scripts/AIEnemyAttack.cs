using UnityEngine;
using System.Collections;

public class AIEnemyAttack : MonoBehaviour {

    public GameObject Bullet;
    public GameObject bulletPos;
    public float speedBullet;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipShoot;

    private string isMusic;
    [SerializeField]
    private Transform startPos, upPos;
    private float shootTimer = 1f;
    float cooldown = 0;

    public bool isMove;
    Vector3 posPlayer, temp;
    int random;
    float cooldownMove = 2f;
    float time;

    private Rigidbody2D myBody;
    private bool iMoveFollowPlayer;
    private bool isMoveY;
    private bool isTurning = true;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        time = cooldownMove / 2;

        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
        //StartCoroutine(Attack());
        //        GetComponent<PhotonView>().RPC("Shoot", PhotonTargets.All); 
    }

    void Update()
    {

        if (GetComponent<EnemyDefence>().isDead)
        {
            return;
        }


        cooldown -= Time.deltaTime;

        float distance = 10f;


        if (transform.localScale.y < 0)
        {
            distance = -10f;
        }
        
        
        RaycastHit2D hit = Physics2D.Raycast(upPos.position, transform.up , distance);
        
        //if (hitwall || hitbrick)
        //{
        //    return;
        //}

        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "stone" || hit.collider.tag == "brick"
                || hit.collider.tag == "victory" || hit.collider.tag == "enemy")
            {
                
            }

            if (hit.collider.tag == "Player")
            {
                cooldownMove = 2f;
                time = cooldownMove / 2;
                iMoveFollowPlayer = false;
                random = Random.Range(0, 2);
                //hit.collider.gameObject.name
                posPlayer = hit.collider.gameObject.transform.position;
                //Debug.Log(hit.collider.gameObject.transform.position);
                AttackNew();
            }
        }

        if (Vector3.Distance(transform.position, posPlayer) < .5f && !iMoveFollowPlayer)
        {
            iMoveFollowPlayer = true;
            temp = transform.position;
            isMove = true;
            GetComponent<EnemyDefence>().isWaiting = true;
            //Debug.Log(random);
            if (random == 0)
            {
                if(GetComponent<EnemyDefence>().isMoveY)
                {
                    //myBody.velocity = new Vector2(-1, 0) * 50f * Time.deltaTime;
                    //transform.Translate(Vector2.left * Time.deltaTime * 5f);
                    transform.localRotation = Quaternion.identity;
                    transform.Rotate(0, 0, 90);
                    transform.localScale = new Vector3(-1, 1, 1);
                    isMoveY = false;
                }
                else
                {
                    //myBody.velocity = new Vector2(0, -1) * 50f * Time.deltaTime;
                    //transform.Translate(Vector2.down * Time.deltaTime * 5f);
                    transform.localRotation = Quaternion.identity;
                    transform.Rotate(0, 0, 0);
                    transform.localScale = new Vector3(1, -1, 1);
                    isMoveY = true;
                }

            }
            if (random == 1)
            {

                if (GetComponent<EnemyDefence>().isMoveY)
                {
                    //myBody.velocity = new Vector2(1, 0) * 50f * Time.deltaTime;
                    //transform.Translate(Vector2.right * Time.deltaTime * 5f);
                    transform.localRotation = Quaternion.identity;
                    transform.Rotate(0, 0, -90);
                    transform.localScale = new Vector3(1, 1, 1);
                    isMoveY = false;
                }
                else
                {
                    //myBody.velocity = new Vector2(0, 1) * 50f * Time.deltaTime;
                    //transform.Translate(Vector2.up * Time.deltaTime * 5f);
                    transform.localRotation = Quaternion.identity;
                    transform.Rotate(0, 0, 0);
                    transform.localScale = new Vector3(1, 1, 1);
                    isMoveY = true;
                }
            }
        }

       
        if (isMove)
        {
            time -= Time.deltaTime;
            cooldownMove -= Time.deltaTime;
            if(time < 0 && isTurning)
            {
                isTurning = false;
                Vector3 temp = transform.localScale;

                if (isMoveY)
                {
                    if (temp.y == 1f)
                    {
                        temp.y = -1f;
                        //myBody.velocity = new Vector2(0, -1) * 50f * Time.deltaTime;
                        transform.localRotation = Quaternion.identity;
                        transform.Rotate(0, 0, 0);
                        transform.localScale = new Vector3(1, -1, 1);
                    }
                    else
                    {
                        temp.y = 11f;
                        //myBody.velocity = new Vector2(0, 1) * 50f * Time.deltaTime;
                        transform.localRotation = Quaternion.identity;
                        transform.Rotate(0, 0, 0);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    if (temp.x == 1f)
                    {
                        temp.x = -1f;
                        //myBody.velocity = new Vector2(-1, 0) * 50f * Time.deltaTime;
                        transform.localRotation = Quaternion.identity;
                        transform.Rotate(0, 0, 90);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        temp.x = 1f;
                        //myBody.velocity = new Vector2(1, 0) * 50f * Time.deltaTime;
                        transform.localRotation = Quaternion.identity;
                        transform.Rotate(0, 0, -90);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }  
               
            }
            if (cooldownMove < 0)
            {
                isMove = false;
                GetComponent<EnemyDefence>().isWaiting = false;
                //Debug.Log(isMove);
            }
            
        }
        
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
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

    void AttackNew()
    {
        if (cooldown > 0)
        {
            return;
        }
        else
        {

            GameObject bullet = ObjectPooler.instance.GetPooledBulletEnemy();
            if (bullet == null) return;

            bullet.transform.position = bulletPos.transform.position;
            bullet.transform.rotation = bulletPos.transform.rotation;
            bullet.SetActive(true);

            //var bullet = (GameObject)Instantiate(Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
            var bulletBody = (Rigidbody2D)bullet.GetComponentInChildren(typeof(Rigidbody2D));
            //            bulletBody.AddForce( bulletPos.transform.up * speedBullet, ForceMode2D.Impulse);
            bulletBody.velocity = bulletPos.transform.up * speedBullet;

            if (transform.localScale.y < 0)
            {
                bulletBody.velocity = bulletPos.transform.up * -speedBullet;
            }

            audioSource.PlayOneShot(clipShoot);
            cooldown = shootTimer;
        }
       
    }

}
