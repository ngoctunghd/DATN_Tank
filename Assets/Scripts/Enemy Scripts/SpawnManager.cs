using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager instance;

    public GameObject[] spawnEnemy;

    private GameObject[] enemies;

    private int countEnemy ;

    [HideInInspector]
    public int currentEnemy;

	// Use this for initialization
	void Awake () {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        countEnemy = currentEnemy = enemies.Length;
        MakeInstance();
	}

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (currentEnemy < countEnemy)
        {
            int random = Random.Range(0, spawnEnemy.Length);
            spawnEnemy[random].GetComponent<SpawnEnemy>().Spawn(true);
            currentEnemy++; 
        }
	}

    public void DecreaseCurrentEnemy()
    {
        currentEnemy--;
    }
}
