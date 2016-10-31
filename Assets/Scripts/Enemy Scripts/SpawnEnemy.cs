using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public static SpawnEnemy instance;

    public GameObject[] enemies;

    private Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(Delay());        
	}

    IEnumerator Delay() {
        anim.Play("Spawner");
        yield return new WaitForSeconds(0.8f);

        Spawn(true);
    }  
   
    public void Spawn(bool value)
    {
        if (value)
        {

            int ran = Random.Range(0, 12);
            if (ran == 11)
            {
                Instantiate(enemies[5], transform.position, Quaternion.identity);
            }
            else
            {
                int random = Random.Range(0, 5);
                Instantiate(enemies[random], transform.position, Quaternion.identity);
            }
        }   
 
    }
}
