using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float minX, maxX, minY, maxY;

    private string isMusic;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        isMusic = PlayerPrefs.GetString("Music");
        
        if (isMusic == "Off")
        {
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }

    }

    void Update()
    {
        if (player.GetComponent<HealthPlayer>().isDead)
        {
            return;
        }

        Vector3 temp = transform.position;
        temp.x = target.position.x;
        temp.y = target.position.y;

        if (temp.x < minX)
        {
            temp.x = minX;
        }

        if (temp.x > maxX)
        {
            temp.x = maxX;
        }

        if (temp.y < minY)
        {
            temp.y = minY;
        }

        if (temp.y > maxY)
        {
            temp.y = maxY;
        }
        transform.position = temp;
    }
}
