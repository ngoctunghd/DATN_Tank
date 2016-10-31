using UnityEngine;
using System.Collections;

public class OnlineModeGameManager : MonoBehaviour {

    public static OnlineModeGameManager instance { get; set; }

    private PhotonView _photonView;

    void Awake()
    {
        instance = this;

        _photonView = GetComponent<PhotonView>();
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    private void makeDemoData()
    {

        if (PhotonNetwork.isMasterClient)
        {
            
        }
        else
        {
            
        }
    }

}
