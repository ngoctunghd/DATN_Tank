using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworksManager : MonoBehaviour {

    public static NetworksManager instance;

    public string playerPrefab1 = "Player3";
    public string playerPrefab2 = "Player2";
    public string[] enemyPrefab;
    public Transform[] posEnemy;
    public Transform[] posPlayer;

    private string isNetwork;

    private PhotonView photonView;

    public GameObject pausePanel;
    public Text timeText;
    public Button pauseButton;

    private float timeLeft;

    void Awake()
    {
        MakeInstance();
        photonView = GetComponent<PhotonView>();
       
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
        Time.timeScale = 1;
        timeLeft = 0;

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab1, posPlayer[0].position, Quaternion.identity, 0);
            PhotonNetwork.Instantiate(enemyPrefab[0], posEnemy[0].position, Quaternion.identity, 0);
            PhotonNetwork.Instantiate(enemyPrefab[1], posEnemy[1].position, Quaternion.identity, 0);
            if(PhotonNetwork.room.customProperties["map"].ToString() == "Map 4")
            {
                PhotonNetwork.Instantiate(enemyPrefab[2], posEnemy[2].position, Quaternion.identity, 0);
            }

        }
            if (PhotonNetwork.isNonMasterClientInRoom)
            {
                PhotonNetwork.Instantiate(playerPrefab2, posPlayer[1].position, Quaternion.identity, 0);
            }
    }

    void Update()
    {
        if (timeLeft < 0)
        {
            return;
        }

        if (timeLeft > 0 && timeLeft <= 0.5f)
        {
            timeLeft -= 0.1f;
            Resume();
        }

        if (timeLeft <= 20f && timeLeft >= 0.5f)
        {
            if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("TimeUpdate", PhotonTargets.All);
                PhotonNetwork.networkingPeer.SendOutgoingCommands();
            }
        }
    }

    public void Pause()
    {
        photonView.RPC("ShowPausePanel", PhotonTargets.All);
        PhotonNetwork.networkingPeer.SendOutgoingCommands();
        
    }

    [PunRPC]
    void ShowPausePanel()
    {
        pauseButton.interactable = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        timeLeft = 20;

    }

    public void Resume()
    {
        photonView.RPC("HidePausePanel", PhotonTargets.All);
        PhotonNetwork.networkingPeer.SendOutgoingCommands();
    }

    [PunRPC]
    void HidePausePanel()
    {
        pauseButton.interactable = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void Home()
    {
        photonView.RPC("LoadSceneMenu", PhotonTargets.All);
        PhotonNetwork.networkingPeer.SendOutgoingCommands();
    }

    [PunRPC]
    void LoadSceneMenu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    [PunRPC]
    void TimeUpdate()
    {
        timeLeft -= 0.03f;
        timeText.text = (int)timeLeft + "";
    }

}
