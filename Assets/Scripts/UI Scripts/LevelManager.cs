using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    [System.Serializable]
    public class Level
    {

        public string levelText;
        public int Unlocked;
        public bool isInteractable;

        public Button.ButtonClickedEvent OnClickEvent;

    }
    public GameObject levelButton;
    public Transform Spacer;
    public List<Level> levelList;

    public GameObject loadingPanel;
    public Image loadingBar;
    private AsyncOperation async;

    private float ratioScore;
    private int levelNum;
    public int bannerId;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    // Use this for initialization
    void Start()
    {

        //      DeleteAll();
        FillList();

    }


    void FillList()
    {
        //return;
        foreach (var level in levelList)
        {
            GameObject newbutton = Instantiate(levelButton) as GameObject;
            newbutton.GetComponent<RectTransform>().parent = Spacer.GetComponent<RectTransform>();
            newbutton.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            newbutton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


            LevelButton button = newbutton.GetComponent<LevelButton>();
            button.levelText.text = level.levelText;

            if (PlayerPrefs.GetInt("Level " + button.levelText.text) == 1)
            {
                level.Unlocked = 1;
                level.isInteractable = true;
            }

            button.unlocked = level.Unlocked;
            button.GetComponent<Button>().interactable = level.isInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => loadLevel("Level " + button.levelText.text));

            Time.timeScale = 1;

            // Set Star for each Level

            int highScore = PlayerPrefs.GetInt("Level " + button.levelText.text + "highScoreLevel");

            levelNum = Int32.Parse(button.levelText.text);
            if (levelNum > 3)
            {
                ratioScore = levelNum * 0.25f;
            }
            else
            {
                ratioScore = 1;
            }

            if (highScore > 0)
            {
                button.Star1.SetActive(true);
            }

            if (highScore >= (80*ratioScore))
            {
                button.Star2.SetActive(true);
            }

            if (highScore >= (150 * ratioScore))
            {
                button.Star3.SetActive(true);
            }
            
        }

        SaveAll();
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level " + button.levelText.text, button.unlocked);

        }
    }

    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void loadLevel(string value)
    {

        StartCoroutine(LoadLevel(value));
    }

    IEnumerator LoadLevel(string value)
    {
        //        Application.LoadLevel(value);
        loadingPanel.SetActive(true);
        //        SceneManager.LoadScene(value);
        async = SceneManager.LoadSceneAsync(value);
        while (!async.isDone)
        {
            loadingBar.fillAmount = async.progress;
            yield return null;
        }

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
