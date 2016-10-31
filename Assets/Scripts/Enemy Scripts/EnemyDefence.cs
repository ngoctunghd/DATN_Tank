using UnityEngine;
using System.Collections;

public class EnemyDefence : MonoBehaviour {

    private Rigidbody2D myBody;

    public Transform[] waypoints;
    int cur = 0;

    public float speed = 0.3f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
       
    }

    // Use this for initialization
    void Start () {
	
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            myBody.MovePosition(p);
            

        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;

        // Animation
        Vector2 dir = waypoints[cur].position - transform.position;
        if (dir.y > 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir.y < 0)
        {
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, -1, 1);
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
}
