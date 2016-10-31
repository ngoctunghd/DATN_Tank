using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;
    public Button btnMusic;

    public GameObject ButtonPanel, InfoPanel, networkPanel, QuitPanel;

    private string isMusic;


    void Awake()
    {
       
    }

	// Use this for initialization
	void Start () {
        isMusic = PlayerPrefs.GetString("Music");
        if (isMusic == "Off")
        {
            btnMusic.GetComponent<Image>().sprite = OffSprite;
        }
        else
        {
            btnMusic.GetComponent<Image>().sprite = OnSprite;
        }
    }


    public void Play() {
        SceneManager.LoadScene("ChooseLevel");

        //reset mang tank
        PlayerPrefs.SetInt("Life", 2);
        PlayerPrefs.SetString("isNetwork", "false");
    }

    public void ChangeMusic()
    {

        if (isMusic == "Off")
        {
            PlayerPrefs.SetString("Music", "On");
            isMusic = "On";
            btnMusic.GetComponent<Image>().sprite = OnSprite;
            //print(isMusic);
        }
        else
        {
            PlayerPrefs.SetString("Music", "Off");
            isMusic = "Off";
            btnMusic.GetComponent<Image>().sprite = OffSprite;
            //print(isMusic);
        }

    }

    public void ShowInfo()
    {
        ButtonPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        ButtonPanel.SetActive(true);
        InfoPanel.SetActive(false);
        networkPanel.SetActive(false);
        AdManager.instance.HideBanner();
    }

    public void Exit()
    {
        QuitPanel.SetActive(true);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        QuitPanel.SetActive(false);
    }

    public void MultiPlayer()
    {
        networkPanel.SetActive(true);
        ButtonPanel.SetActive(false);
        AdManager.instance.showBanner();
        //if (isNetwork == false)
        //{
        //    networkPanel.SetActive(true);
        //    isNetwork = true;
        //    PlayerPrefs.SetString("isNetwork", "true");
        //    PhotonNetwork.automaticallySyncScene = true;
        //    PhotonNetwork.autoJoinLobby = true;
        //    PhotonNetwork.ConnectUsingSettings(VERSION);

        //    //reset mang tank
        //    PlayerPrefs.SetInt("Life", 2);
        //}
    }
}
