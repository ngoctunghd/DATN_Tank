using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler instance;
    public GameObject pooledBulletEnemy;
    public int pooledAmount = 10;
    public bool willGrow;

    List<GameObject> pooledBulletEnemys;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        pooledBulletEnemys = new List<GameObject>();
        //pooledObjects1 =  GameObject.FindGameObjectsWithTag("rocket");
        //Debug.Log(pooledObjects1.Length);

        for (int i = 0; i < pooledAmount; i++)
        {

            GameObject obj1 = (GameObject)Instantiate(pooledBulletEnemy);
            obj1.SetActive(false);
            pooledBulletEnemys.Add(obj1);

        }

    }

    public GameObject GetPooledBulletEnemy()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            if (!pooledBulletEnemys[i].activeInHierarchy)
            {
                return pooledBulletEnemys[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledBulletEnemy);
            pooledBulletEnemys.Add(obj);
            return obj;
        }

        return null;
    }

}
