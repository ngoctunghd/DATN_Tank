using UnityEngine;
using System.Collections;

public class Active : MonoBehaviour {

    private GameObject itemShovel;

    private SpriteRenderer spriteStone;
    private BoxCollider2D boxStone;

    void Awake()
    {
//        itemShovel = GameObject.FindGameObjectWithTag("Item");
        spriteStone = GetComponent<SpriteRenderer>();
        boxStone = GetComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectWithTag("ItemShovel") == null)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag("ItemShovel").GetComponent<Item>().isActiveStone)
        {
            StartCoroutine(Swap());
           
        }
        
	}

    IEnumerator Swap()
    {
        yield return new WaitForSeconds(8f);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StonePlayer"))
        {
            go.GetComponent<SpriteRenderer>().enabled = false;
            go.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
