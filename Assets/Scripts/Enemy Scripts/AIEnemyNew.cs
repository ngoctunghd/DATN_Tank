using UnityEngine;
using System.Collections;

public class AIEnemyNew : MonoBehaviour {

    private const string CHILD = "barrelBeige_outline";

    private Rigidbody2D myBody;

    public Transform enemy;

    private Transform player;

    public float speed = 10f;

    [SerializeField]
    private Transform upPos;
    [SerializeField]
    private bool isMove;

    public GameObject[] items;
    private bool colupEnemy;

    private SpriteRenderer sprite;

    [SerializeField]
    private int healthBoss;
    private float timeSpeed;
    private float timeIsMove;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        myBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player == null)
        {
            return;
        }
     
    }

	// Use this for initialization
	void Start () {
        isMove = true;
        healthBoss = 2;

    }

    void Update()
    {
        timeSpeed -= Time.deltaTime;
        if (timeSpeed <= 0)
        {
            speed = 100f;
        }

        timeIsMove -= Time.deltaTime;
        if(timeIsMove <= 0)
        {
            isMove = true;
        }
            
    }

	// Update is called once per frame
	void FixedUpdate () {

        colupEnemy = Physics2D.Linecast(enemy.position, upPos.position, 1 << LayerMask.NameToLayer("enemy"));
        //Debug.DrawLine(enemy.position, upPos.position, Color.cyan);

        if (colupEnemy)
        {
            return;
//            speed = 0;
        }
        else
        {

        }

        //Debug.DrawLine(enemy.position, player.position, Color.red);

        SmartAI();
//        SetFacing();
        if(healthBoss == 1)
        {
            sprite.color = new Color(135f / 255f, 0 / 255f, 255f / 255f, 255f / 255f);
            transform.FindChild(CHILD).GetComponent<SpriteRenderer>().color = sprite.color;
        }

        if(healthBoss <= 0)
        {
            SpawnManager.instance.DecreaseCurrentEnemy();
            GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();
            Destroy(gameObject);
        }
        
	}

    void SmartAI() {
        if (isMove)
        {
            Vector3 temp = player.position - enemy.position;
            float x = temp.x;
            float y = temp.y;
            if (x > 0.1f)
            {
                myBody.velocity = new Vector2(1, 0) * speed * Time.deltaTime;
            }
            else if (x < -0.1f)
            {
                myBody.velocity = new Vector2(-1, 0) * speed * Time.deltaTime;
            }
            else
            {
                if (y > 0.1f)
                {
                    myBody.velocity = new Vector2(0, 1) * speed * Time.deltaTime;
                }
                else if (y < -0.1f)
                {
                    myBody.velocity = new Vector2(0, -1) * speed * Time.deltaTime;
                }
                else
                {
                   
                }
            }
            SetFacing();
        }
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

    IEnumerator Move()
    {
        if (transform.localRotation.z == 0)
        {
            yield return new WaitForSeconds(3f);
//            Debug.Log(transform.localRotation.z);
        }
//        Debug.Log(transform.localScale);
        transform.localRotation = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(0.5f);

        myBody.velocity = Vector2.down * 100f * Time.deltaTime;
        
    }

    void OnCollisionEnter2D(Collision2D target) {
        if (target.gameObject.tag == "Player" ) {
            speed = 0f;
            timeSpeed = 4f;           
        }

        if (target.gameObject.tag == "stone" || target.gameObject.tag == "brickEnemy"
            || target.gameObject.tag == "Boss" || target.gameObject.tag == "Water")
        {
            isMove = false;
            StartCoroutine(Move());
            timeIsMove = 5f;
        }
    }

    void OnCollisionExit2D(Collision2D target) {
        if (target.gameObject.tag == "Player" || target.gameObject.tag == "enemy")
        {
            speed = 100f;
            
        }
        if (target.gameObject.tag == "stone" || target.gameObject.tag == "brickEnemy"
            || target.gameObject.tag == "Boss" || target.gameObject.tag == "Water")
        {
            isMove = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bulletplayer")
        {
            --healthBoss;
            if (healthBoss > 0)
            {
                int random = Random.Range(0, 9);
                if (random == 0)
                {
                    Instantiate(items[0], transform.position, Quaternion.identity);
                }
                else if (random == 1)
                {
                    Instantiate(items[1], transform.position, Quaternion.identity);
                }
                else if (random == 2)
                {
                    Instantiate(items[2], transform.position, Quaternion.identity);
                }
                else if (random == 3)
                {
                    Instantiate(items[3], transform.position, Quaternion.identity);
                }
                else if (random == 4)
                {
                    Instantiate(items[4], transform.position, Quaternion.identity);
                }
                else
                {
                    return;
                }

            }
        }

        if (collision.tag == "enemy")
        {
            speed = 0f;
            timeSpeed = 4f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            speed = 100f;
        }

        if (collision.tag == "Player")
        {
            speed = 100f;
        }
    }

}
