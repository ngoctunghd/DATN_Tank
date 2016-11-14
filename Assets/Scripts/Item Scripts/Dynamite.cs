using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {

    public static Dynamite instance;

    public GameObject explosion;

    private SpriteRenderer spriteItem;
    private BoxCollider2D boxItem;

    void Awake()
    {
        MakeInstance();
        spriteItem = GetComponent<SpriteRenderer>();
        boxItem = GetComponent<BoxCollider2D>();

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        boxItem.enabled = false;
        StartCoroutine(SetBox2D());
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "enemy")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();
            CameraShake.instance.Shake(0.002f, 0.01f);
            Destroy(gameObject);
           
        }

        if (target.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            HealthPlayer.instane.SendMessage("HealthUpdate", 1, SendMessageOptions.DontRequireReceiver);
            //           health.SendMessage("HealthUpdate", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator SetBox2D()
    {
        yield return new WaitForSeconds(0.8f);
        boxItem.enabled = true;
    }
}
