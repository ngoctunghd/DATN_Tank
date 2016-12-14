using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BulletEnemy : MonoBehaviour {

    private HealthPlayer health;
    private int damage = 1;
    private GameObject gameManager;

    public GameObject explosion;

    public GameObject explosionBG, explosionPlayer;

    //    public GameObject player;

    void Awake()
    {
//        health = player.GetComponent<HealthPlayer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) health = player.GetComponent<HealthPlayer>();
       
        gameManager = GameObject.Find("GamePlayManager");

    }

    // Use this for initialization
    void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "stone" || target.tag == "StonePlayer")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy();
        }

        if (target.tag == "brick")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy();
            Destroy(target.gameObject);
        }

        if (target.tag == "brickEnemy")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy();
        }

        if (target.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy();
            HealthPlayer.instane.SendMessage("HealthUpdate", damage, SendMessageOptions.DontRequireReceiver);
 //           health.SendMessage("HealthUpdate", damage, SendMessageOptions.DontRequireReceiver);
        }

        if (target.tag == "gameOver") {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);        
            CameraShake.instance.Shake(0.002f, 0.05f);
            gameManager.GetComponent<GamePlayManager>().GameOver();
            Destroy();
        }

        if (target.tag == "Rocket")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy();
            Destroy(target.gameObject);
        }

        if (target.tag == "Amor")
        {
            Destroy();
        }


        if (target.tag == "victory")
        {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            CameraShake.instance.Shake(0.002f, 0.04f);
            gameManager.GetComponent<GamePlayManager>().Victory();
            PlayerPrefs.SetInt("Level " + (SceneManager.GetActiveScene().buildIndex), 1);
            GamePlayManager.instance.scoreBonus();

            Destroy();

        }

        if (target.tag == "bulletplayer")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy();
            Destroy(target.gameObject);
        }

        if (target.tag == "bulletenemy")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy();
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
