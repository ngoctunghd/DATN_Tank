using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float maxX, minY, maxY;

    private string isMusic;

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
        Vector3 temp = transform.position;
        temp.x = target.position.x;
        temp.y = target.position.y;

        if (temp.x < -maxX)
        {
            temp.x = -maxX;
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
