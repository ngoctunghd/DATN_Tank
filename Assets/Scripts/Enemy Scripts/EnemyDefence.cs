using UnityEngine;
using System.Collections;

public class EnemyDefence : MonoBehaviour {

    public Waypoint[] wayPoints;
    public float speed = 3f;
    public bool isCircular;
    // Always true at the beginning because the moving object will always move towards the first waypoint
    public bool inReverse = true;

    private Waypoint currentWaypoint;
    private int currentIndex = 0;
    public bool isWaiting = false;
    private float speedStorage = 0;

    private Rigidbody2D myBody;
    private SpriteRenderer mySprite;
    private BoxCollider2D myBox;
    public GameObject[] items;

    public bool isDead;
    public Transform downPos, rightPos, leftPos;
    private float waitingTimer = 2f;
    private float cooldown = 0;

    public bool isMoveY;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myBox = GetComponent<BoxCollider2D>();
    }


    /**
	 * Initialisation
	 * 
	 */
    void Start()
    {
        if (wayPoints.Length > 0)
        {
            currentWaypoint = wayPoints[0];
        }

        isDead = false;
    }



    /**
	 * Update is called once per frame
	 * 
	 */
    void Update()
    {
        float distanceDown = -2f;
        float distanceRight = 2f;

        if (transform.localScale.y < 0)
        {
            distanceDown = 2f;
        }

        if (transform.localScale.x < 0)
        {
            distanceRight = -2f;
        }
        Debug.DrawRay(downPos.position, transform.up * distanceDown, Color.red);
        Debug.DrawRay(rightPos.position, -transform.right * distanceRight, Color.green);
        Debug.DrawRay(leftPos.position, transform.right * distanceRight, Color.blue);
        RaycastHit2D hitDown = Physics2D.Raycast(downPos.position, transform.up, distanceDown);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPos.position, -transform.right, distanceRight);
        RaycastHit2D hitLeft = Physics2D.Raycast(leftPos.position, transform.right, distanceRight);

        if (hitDown.collider != null)
        {
            if (hitDown.collider.tag == "Player")
            {
                isWaiting = true;
                cooldown = waitingTimer;
                Vector3 temp = transform.localScale;
                if (temp.y < 0)
                {
                    temp.y = 1f;
                }
                else
                {
                    temp.y = -1f;
                }
                transform.localScale = temp;
            }
        }

        if (hitRight.collider != null)
        {
            if (hitRight.collider.tag == "Player")
            {
                isWaiting = true;
                cooldown = waitingTimer;
                transform.localRotation = Quaternion.identity;
                transform.Rotate(0, 0, -90);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.tag == "Player")
            {
                isWaiting = true;
                cooldown = waitingTimer;
                transform.localRotation = Quaternion.identity;
                transform.Rotate(0, 0, 90);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            isWaiting = false;
        }

        if (GetComponent<AIEnemyAttack>().isMove)
        {

            return;
        }

        if (currentWaypoint != null && !isWaiting)
        {
            MoveTowardsWaypoint();
            
        }

        //SetFacing();
    }



    /**
	 * Pause the mover
	 * 
	 */
    void Pause()
    {
        isWaiting = !isWaiting;
    }



    /**
	 * Move the object towards the selected waypoint
	 * 
	 */
    private void MoveTowardsWaypoint()
    {
        // Get the moving objects current position
        Vector3 currentPosition = this.transform.position;

        // Get the target waypoints position
        Vector3 targetPosition = currentWaypoint.transform.position;

        // If the moving object isn't that close to the waypoint
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {

            // Get the direction and normalize
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();



            //scale the movement on each axis by the directionOfTravel vector components
            this.transform.Translate(
                directionOfTravel.x * speed * Time.deltaTime,
                directionOfTravel.y * speed * Time.deltaTime,
                directionOfTravel.z * speed * Time.deltaTime,
                Space.World
            );

            float x = directionOfTravel.x;
            float y = directionOfTravel.y;

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
                isMoveY = false;
            }
            else if (x < 0)
            {
                transform.localRotation = Quaternion.identity;
                transform.Rotate(0, 0, 90);
                transform.localScale = new Vector3(-1, 1, 1);
                isMoveY = false;
            }
            else if (y > 0)
            {
                transform.localRotation = Quaternion.identity;
                transform.Rotate(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                isMoveY = true;
            }
            else if (y < 0)
            {
                transform.localRotation = Quaternion.identity;
                transform.Rotate(0, 0, 0);
                transform.localScale = new Vector3(1, -1, 1);
                isMoveY = true;
            }
        }
        else
        {

            // If the waypoint has a pause amount then wait a bit
            if (currentWaypoint.waitSeconds > 0)
            {
                Pause();
                Invoke("Pause", currentWaypoint.waitSeconds);
            }

            // If the current waypoint has a speed change then change to it
            if (currentWaypoint.speedOut > 0)
            {
                speedStorage = speed;
                speed = currentWaypoint.speedOut;
            }
            else if (speedStorage != 0)
            {
                speed = speedStorage;
                speedStorage = 0;
            }

            NextWaypoint();
        }
    }



    /**
	 * Work out what the next waypoint is going to be
	 * 
	 */
    private void NextWaypoint()
    {
        if (isCircular)
        {

            if (!inReverse)
            {
                currentIndex = (currentIndex + 1 >= wayPoints.Length) ? 0 : currentIndex + 1;
            }
            else
            {
                currentIndex = (currentIndex == 0) ? wayPoints.Length - 1 : currentIndex - 1;
            }

        }
        else
        {

            // If at the start or the end then reverse
            if ((!inReverse && currentIndex + 1 >= wayPoints.Length) || (inReverse && currentIndex == 0))
            {
                inReverse = !inReverse;
            }
            currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;

        }

        currentWaypoint = wayPoints[currentIndex];
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "bulletplayer" || target.tag == "dynamite")
        {
            isDead = true;
            Destroy();
            StartCoroutine(SpawnEnemy());

            int random = Random.Range(0, 10);
            if (random == 0 || random == 2)
            {
                Instantiate(items[0], transform.position, Quaternion.identity);
            }
            else if (random == 1)
            {
                Instantiate(items[1], transform.position, Quaternion.identity);
            }
            else if (random >= 3 || random <= 5)
            {
                Instantiate(items[2], transform.position, Quaternion.identity);
            }
            else
            {
                return;
            }
        }
    }

    void Destroy()
    {
        mySprite.enabled = false;
        myBox.enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f);

        mySprite.enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.position = wayPoints[0].transform.position;
        currentIndex = 0;
        currentWaypoint = wayPoints[0];
        yield return new WaitForSeconds(0.5f);
        myBox.enabled = true;
        isDead = false;
       
    }
}
