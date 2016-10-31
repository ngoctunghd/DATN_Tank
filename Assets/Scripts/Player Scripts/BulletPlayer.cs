using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BulletPlayer : MonoBehaviour {

    public GameObject explosion;

    public GameObject explosionBG, explosionPlayer;
    
    private GamePlayManager gameManager;
    private PhotonView photonView;


    void Awake() {
        photonView = GetComponent<PhotonView>();
        if(GameObject.Find("GamePlayManager") == null)
        {
            return;
        }
        gameManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();    
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Destroy")
        {
            Destroy(gameObject);
            Instantiate(explosionBG, transform.position, Quaternion.identity);
        }

        if (target.tag == "stone" || target.tag == "StonePlayer")
        {
            if (gameObject.tag == "Rocket")
            {
                Instantiate(explosionBG, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(target.gameObject);
            }
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (target.tag == "brick")
        {
            Instantiate(explosionBG, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(target.gameObject);
        }

        if (target.tag == "brickEnemy")
        {
            Destroy(gameObject);
            Destroy(target.gameObject);
            Instantiate(explosionBG, transform.position, Quaternion.identity);
        }

        if (target.tag == "enemy")
        {

            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
            if (!PhotonNetwork.connected)
            {
                Destroy(target.gameObject);
            }
            SpawnManager.instance.DecreaseCurrentEnemy();
            GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();         
            CameraShake.instance.Shake(0.002f, 0.01f);
            
        }

        if (target.tag == "Boss")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
           
            CameraShake.instance.Shake(0.002f, 0.01f);

            if (gameObject.tag == "Rocket")
            {

                Instantiate(explosion, transform.position, Quaternion.identity);

                Destroy(gameObject);
                if (!PhotonNetwork.connected)
                {
                    Destroy(target.gameObject);
                }
                SpawnManager.instance.DecreaseCurrentEnemy();
                GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>().scoreUpdate();
                CameraShake.instance.Shake(0.002f, 0.01f);
            }
        }


        if (target.tag == "victory") {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            CameraShake.instance.Shake(0.002f, 0.04f);
            gameManager.Victory();
            PlayerPrefs.SetInt("Level " + (SceneManager.GetActiveScene().buildIndex), 1);
            GamePlayManager.instance.scoreBonus();

            Destroy(gameObject);
                               
        }

        if (target.tag == "gameOver")
        {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);        
            CameraShake.instance.Shake(0.002f, 0.05f);
            gameManager.GameOver();
            Destroy(gameObject);
        }

        if (target.tag == "Player")
        {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            CameraShake.instance.Shake(0.002f, 0.05f);
            Destroy(gameObject);
        }


    }

}
