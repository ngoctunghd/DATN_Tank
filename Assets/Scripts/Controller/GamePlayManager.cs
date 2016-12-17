using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GamePlayManager : MonoBehaviour {

    public static GamePlayManager instance;

    public Text scoreText;

    [HideInInspector]
    public int score;

    public Text scoreRS, bestScoreRS, bestScore, scoreOver;

    public Text Life;

    [SerializeField]
    private GameObject pausePanel, victoryPanel, gameOverPanel, questionPanel;

    private GameObject player;

    public GameObject[] stoneEnemies;

    [SerializeField]
    private int StoneToBrick;

    [SerializeField]
    private Image Star1, Star2, Star3;

    private Victory victory;

    //public GAME_STATE gameState { get; set; }

    private int hourNow;

    private float ratioScore;
    private int levelNum;
    private float timeShowAd;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        victory = GameObject.FindGameObjectWithTag("victory").GetComponent<Victory>();
        MakeInstance();
    }

    void MakeInstance() {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
# if UNITY_ANDROID || UNITY_IOS
        //if (AdManager.instance.BannerOnScreen() || AdManager.instance.BannerIsLoad())
        //{
        //    AdManager.instance.HideBanner();
        //}
#endif        
        timeShowAd = 3f;

        
        levelNum = SceneManager.GetActiveScene().buildIndex - 1;

        score = 0;
        int life = PlayerPrefs.GetInt("Life");
        Life.text = life + " x";

        //SetStoneEnemyFollowTime();


        if(levelNum > 3)
        {
            ratioScore = levelNum * 0.25f;
        }
        else
        {
            ratioScore = 1;
        }
        
    }

    // set stoneEnemies daytime or night
    void SetStoneEnemyFollowTime()
    {
        hourNow = DateTime.Now.Hour;
        if (hourNow > 6 && hourNow <= 18)
        {
            stoneEnemies[0].SetActive(true);
            stoneEnemies[1].SetActive(false);
        }

        if (hourNow > 18 && hourNow <= 23 || hourNow >= 0 && hourNow <= 6)
        {
            stoneEnemies[0].SetActive(false);
            stoneEnemies[1].SetActive(true);
        }
    }

    void DeactiveStoneEnemy()
    {
        stoneEnemies[0].SetActive(false);
        stoneEnemies[1].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID || UNITY_IOS
        //if (AdManager.instance.BannerOnScreen())
        //{
        //    AdManager.instance.HideBanner();
        //}
#endif
        scoreText.text = score + "";

        // Set Star
        //        SetHighScoreLevel();   


        int life = PlayerPrefs.GetInt("Life");
        if (life < 0)
        {

            life = 0;
            GameOver();
        }
        Life.text = HealthPlayer.instane.getCountLife() + " x";

        if (score > StoneToBrick * 10)
        {
            DeactiveStoneEnemy();
        }

        GameOver1();

        if (victory.isVictory)
        {
            Result();

            if (score > 0)
            {

                Star1.GetComponent<Image>().enabled = true;
            }

            if (score >= (80*ratioScore))
            {
                Star2.GetComponent<Image>().enabled = true;
            }

            if (score >= (150*ratioScore))
            {
                Star3.GetComponent<Image>().enabled = true;
            }
        }

	}

    public void scoreUpdate()
    {
        score += 10;
        
    }

    public void scoreBonus()
    {
        score += 20;
    }

    public void Pause() {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
 

    public void Resume() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        

    }

    public void BackToMenu() {

        questionPanel.SetActive(true);

    }

    public void Retry() {
        SceneManager.LoadScene("Level " + (SceneManager.GetActiveScene().buildIndex -1));
        Time.timeScale = 1;

        //reset mang tank
        PlayerPrefs.SetInt("Life", 2);
    }

    void Result()
    {
        scoreRS.text = score + "";
        
        int highScore = PlayerPrefs.GetInt("best");
        //int highScore = PlayerPrefs.GetInt("Level " + (SceneManager.GetActiveScene().buildIndex - 1) + "highScoreLevel");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("best", score);
            //PlayerPrefs.SetInt("Level " + (SceneManager.GetActiveScene().buildIndex - 1) + "highScoreLevel", score);
        }
        bestScoreRS.text = PlayerPrefs.GetInt("best") + "";
        //bestScoreRS.text = PlayerPrefs.GetInt("Level " + (SceneManager.GetActiveScene().buildIndex - 1) + "highScoreLevel");
    }

    void GameOver1()
    {
        scoreOver.text = score + "";
    }

    public void Victory() {
        
         StartCoroutine(DelayVictory());

    }

    public void GameOver() {      
        StartCoroutine(Over());
    }

    private IEnumerator DelayVictory()
    {
        
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
        SetHighScoreLevel();
    }

    private IEnumerator Over()
    {


        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        bestScore.text = PlayerPrefs.GetInt("best") + "";
      
       
    }

    public void LoadSceneLevel() {
        SceneManager.LoadScene("ChooseLevel");
    }

    void SetHighScoreLevel() {
        int highScoreLevel = PlayerPrefs.GetInt("Level " + (SceneManager.GetActiveScene().buildIndex - 1) + "highScoreLevel");
        if (score > highScoreLevel)
        {
            PlayerPrefs.SetInt("Level " + (SceneManager.GetActiveScene().buildIndex - 1) + "highScoreLevel", score);
           
        }
    }

    public void shareClick()
    {
        StartCoroutine(TakeScreenshot());
        
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        string Caption = "My HighScore ";
        UM_ShareUtility.FacebookShare(Caption, tex);
        Destroy(tex);
    }

    public void Yes()
    {
        SceneManager.LoadScene("Menu");

        //reset mang tank
        PlayerPrefs.SetInt("Life", 2);
        //LeaveRoom();
    }

    public void No()
    {
        questionPanel.SetActive(false);
    }

}
