using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public GameObject Bullet;
    public GameObject bulletPos;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clipShoot;

    private string isMusic;

    // Use this for initialization
    void Start()
    {
        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
        StartCoroutine(Attack());
//        GetComponent<PhotonView>().RPC("Shoot", PhotonTargets.All); 
    }

    [PunRPC]
    void Shoot()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        var bullet = (GameObject)Instantiate(Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        var bulletBody = (Rigidbody2D)bullet.GetComponentInChildren(typeof(Rigidbody2D));
        //            bulletBody.AddForce( bulletPos.transform.up * speedBullet, ForceMode2D.Impulse);
        bulletBody.velocity = bulletPos.transform.up * 10;

        if (transform.localScale.y < 0)
        {
            bulletBody.velocity = bulletPos.transform.up * -10;
        }

        audioSource.PlayOneShot(clipShoot);
        StartCoroutine(Attack());
    }

}
