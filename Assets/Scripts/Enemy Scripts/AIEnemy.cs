using UnityEngine;
using System.Collections;

public class AIEnemy : MonoBehaviour {

    [SerializeField]
    private Transform startPos, upPos, downPos;

    private bool colupGround, colupBrick, colupPlayer, coldownGround, coldownBrick, coldownPlayer,
                 colrightPlayer, colleftPlayer ;

    private Rigidbody2D myBody;
    public float speed = 0f;

    private float x;
    private float y;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Move(1.5f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeDirection();
    }

    void ChangeDirection()
    {
        colupGround = Physics2D.Linecast(startPos.position, upPos.position, 1 << LayerMask.NameToLayer("stone"));
        colupBrick = Physics2D.Linecast(startPos.position, upPos.position, 1 << LayerMask.NameToLayer("brick"));
        colupPlayer = Physics2D.Linecast(startPos.position, upPos.position, 1 << LayerMask.NameToLayer("player"));

        coldownGround = Physics2D.Linecast(startPos.position, downPos.position, 1 << LayerMask.NameToLayer("stone"));
        coldownBrick = Physics2D.Linecast(startPos.position, downPos.position, 1 << LayerMask.NameToLayer("brick"));
        coldownPlayer = Physics2D.Linecast(startPos.position, downPos.position, 1 << LayerMask.NameToLayer("player"));

        Debug.DrawLine(startPos.position, upPos.position, Color.green);
        Debug.DrawLine(startPos.position, downPos.position, Color.green);

        Vector3 temp = transform.localScale;

        if (colupGround || colupBrick || colupPlayer)
        {

            if (temp.y == 1f)
            {
                temp.y = -1f;
            }
            else
            {
                temp.y = 1f;
            }
            if (temp.x == 1f)
            {
                temp.x = -1f;
            }
            else
            {
                temp.x = 1f;
            }
            if (coldownGround || coldownBrick)
            {
                int random = Random.Range(1, 3);
                if (random == 1) myBody.velocity = new Vector2(0, transform.localScale.y) * speed ;
                if (random == 2) myBody.velocity = new Vector2(transform.localScale.x, 0) * speed ;
            }
        }

        transform.localScale = temp;
        SetFacing();
    }

    IEnumerator Move( float time)
    {       
            yield return new WaitForSeconds(time);
            int random = Random.Range(1, 3);
            if (random == 1) myBody.velocity = new Vector2(0, transform.localScale.y) * speed;
            if (random == 2) myBody.velocity = new Vector2(transform.localScale.x, 0) * speed;
        
        StartCoroutine(Move(time));
    }


    IEnumerator Move1()
    {
        yield return new WaitForSeconds(.5f);
        int random = Random.Range(1, 3);
        if (random == 1) myBody.velocity = new Vector2(0, transform.localScale.y) * 1.7f;
        if (random == 2) myBody.velocity = new Vector2(transform.localScale.x, 0) * 1.7f;
//        speed = 1.7f;
    }

    private void SetFacing()
    {

        x = myBody.velocity.x;
        y = myBody.velocity.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            y = 0;
        }
        else
        {
            x = 0;
        }

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
            transform.Rotate(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (y < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, 0);
            transform.localScale = new Vector3(1, -1, 1);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            myBody.velocity = new Vector2(0, 0);
//            Move(1f);
            speed = 0;
        }
        if (other.gameObject.tag == "enemy")
        {
//            myBody.velocity = new Vector2(0, 0);      
            Vector3 temp = transform.localScale;

            if (temp.y == 1f)
            {
                temp.y = -1f;
            }
            else
            {
                temp.y = 1f;
            }
            if (temp.x == 1f)
            {
                temp.x = -1f;
            }
            else
            {
                temp.x = 1f;
            }

            transform.localScale = temp;
//            speed = 0;
//            StartCoroutine(Move1());
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speed = 2f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }



}
