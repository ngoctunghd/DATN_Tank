using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
    [SerializeField]
    private Transform startPos, upPos, downPos;

    private bool colupGround, colupBrick, colupPlayer, coldownGround, coldownBrick, coldownPlayer,
                 colrightPlayer, colleftPlayer;

    private Rigidbody2D myBody;
    public float speed = 0f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Move(2f));
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

        if (colupGround || colupBrick)
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
           
        }

        if (coldownPlayer)
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
        }

        transform.localScale = temp;
        //SetFacing();
    }

    IEnumerator Move(float time)
    {
        yield return new WaitForSeconds(time);
        int random = Random.Range(1, 5);
        if (random == 1)
        {
            myBody.velocity = new Vector2(0, 1) * speed;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (random == 2)
        {

            myBody.velocity = new Vector2(0, -1) * speed;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, -1, 1);
        }
        else if (random == 3)
        {

            myBody.velocity = new Vector2(1, 0) * speed;
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, -90);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (random == 4)
        {

            myBody.velocity = new Vector2(-1, 0) * speed;
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, 90);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        StartCoroutine(Move(time));
    }
    
}
