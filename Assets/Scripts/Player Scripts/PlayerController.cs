using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : Photon.MonoBehaviour {

    public BulletConfig defaultbulletConfig;

    private BulletConfig activeBulletConfig;

    public float tankSpeed = 2f;
    private bool moveHorizontal;

    private Rigidbody2D myBody;

    public GameObject bulletPos;

    public float speedBullet;

//    private float shootTimer = 0;
//    private float shootCd = 0.5f;
    private float shootTimer = 0.5f;
    float cooldown = 0;


    public ParticleSystem SmokeSystem;
    
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipShoot;

    private string isMusic;

    float timerChange;

    private HealthPlayer healthPlayer;

    const string CHILD = "barrelRed_outline";

    PhotonView m_PhotonView;

    void Awake()
    {
        moveHorizontal = true;
        myBody = GetComponent<Rigidbody2D>();
        m_PhotonView = GetComponent<PhotonView>();

        if (defaultbulletConfig == null)
        {
            Debug.LogWarning("MovementComponent, misisng default config !!");
            enabled = false;//deactive
        }
        else
        {
            activeBulletConfig = defaultbulletConfig;
        }

        healthPlayer = GetComponent<HealthPlayer>();
    }


    public void ChangeBulletConfig(BulletConfig config)
    {
        activeBulletConfig = config;
        timerChange = 5f;
        
    }

    void Start() {

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

    void Update()
    {
        if (PhotonNetwork.connected && m_PhotonView.isMine == false)
        {
            return;
        }
        timerChange -= Time.deltaTime;
        if (timerChange <= 0)
        {
            activeBulletConfig = defaultbulletConfig;
        }

        cooldown -= Time.deltaTime;
        
    }
  
    private void FixedUpdate()
    {
        if (PhotonNetwork.connected && m_PhotonView.isMine == false)
        {
            return;
        }

        // cai dat getButon de dung joystick
        //if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)) ||
        //    (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
        //{
        //    moveHorizontal = false;
        //}
        //if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) ||
        //    (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W)))
        //{
        //    moveHorizontal = true;
        //}

#if UNITY_EDITOR
        MoveKeyboard();
#endif


        float x = ETCInput.GetAxis("Horizontal");
        float y = ETCInput.GetAxis("Vertical");

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            y = 0;           
        }
        else
        {
            x = 0;
        }

        //float x = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
        //float y = (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0);

        if (x != 0f && y != 0f)
        {
            x = (moveHorizontal ? x : 0f);
            y = (moveHorizontal ? 0f : y);
        }
#if UNITY_ANDROID || UNITY_IOS
        myBody.velocity = new Vector2(x * tankSpeed, y * tankSpeed);
#endif

        SetFacing();
        Shoot();

    }

    private void SetFacing()
    {
        float x = myBody.velocity.x;
        float y = myBody.velocity.y;

        if (x > 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, -90);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, 90);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (y > 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (y < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    public void Shoot()        
    {
       
        if (healthPlayer.isDead == false)
        {
            if (cooldown > 0)
            {
                return;
            }

           
            if (Input.GetKeyDown(KeyCode.Space) || ETCInput.GetButton("Fire") )
            {
                //   ShootBullet();

                if (PhotonNetwork.connected)
                {
                    m_PhotonView.RPC("ShootBullet", PhotonTargets.All);
                }
                else
                {
                    ShootBullet();
                }

                if (SmokeSystem != null)
                {
                    var smoke = (ParticleSystem)Instantiate(SmokeSystem, SmokeSystem.transform.position, bulletPos.transform.rotation);
                    smoke.Play();
                    Destroy(smoke.gameObject, smoke.startLifetime * 1.1f);
                }
                audioSource.PlayOneShot(clipShoot);
                cooldown = shootTimer;
            }
        }
        
        
    }

    [PunRPC]
    private void ShootBullet()
    {
        // thay
        var bullet = (GameObject)Instantiate(activeBulletConfig.bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        var bulletBody = (Rigidbody2D)bullet.GetComponentInChildren(typeof(Rigidbody2D));
        // bulletBody.AddForce( bulletPos.transform.up * speedBullet, ForceMode2D.Impulse);
                

        if (transform.localScale.y >= 0)
        {
             bulletBody.velocity = bulletPos.transform.up * speedBullet;
            bullet.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

        }
        else
        {
            bulletBody.velocity = bulletPos.transform.up * -speedBullet;
            bullet.GetComponent<Transform>().localScale = new Vector3(1, -1, 1);
        }
               
    }

    

    void MoveKeyboard()
    {
        float a = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
        float b = (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0);

        if (a != 0f && b != 0f)
        {
            a = (moveHorizontal ? a : 0f);
            b = (moveHorizontal ? 0f : b);
        }

        myBody.velocity = new Vector2(a * tankSpeed, b * tankSpeed);
    }

}
