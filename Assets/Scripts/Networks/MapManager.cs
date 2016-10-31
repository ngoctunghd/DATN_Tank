using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapManager : Photon.MonoBehaviour {
    public static MapManager instance;

    public GameObject map1, map2, map3, map4, map5;

    [HideInInspector]
    public string nameMap;

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
    void Start () {
        map1.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        map2.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map3.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map4.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map5.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        nameMap = "Map 1";

    }

    public void ChooseMap1()
    {
        map1.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        map2.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map3.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map4.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map5.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        nameMap = "Map 1";

    }

    public void ChooseMap2()
    {
        map1.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map2.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        map3.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map4.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map5.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        nameMap = "Map 2";

    }

    public void ChooseMap3()
    {
        map1.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map2.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map3.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        map4.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map5.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        nameMap = "Map 3";

    }


    public void ChooseMap4()
    {
        map1.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map2.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map3.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map4.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        map5.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        nameMap = "Map 4";

    }

    public void ChooseMap5()
    {
        map1.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map2.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map3.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map4.GetComponent<Image>().color = new Color(118f / 255f, 204f / 255f, 255f / 255f, 255f / 255f);
        map5.GetComponent<Image>().color = new Color(0f / 255f, 76f / 255f, 255f / 255f, 178f / 255f);
        nameMap = "Map 5";

    }

}
