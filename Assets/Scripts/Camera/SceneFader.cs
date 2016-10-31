using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;

    [SerializeField]
    private GameObject fadeCanvas;

    [SerializeField]
    private Animator fadeAnim;

    void Awake()
    {
        MakeASingleInstance();
    }

    void MakeASingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator FadeInAnimate(string level)
    {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f));
        SceneManager.LoadScene(level);
//        Application.LoadLevel(level);
        FadeOut();
    }

    IEnumerator FadeOutAnimate()
    {
        fadeAnim.Play("FadeOut");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }

    public void FadeIn(string level)
    {
        StartCoroutine(FadeInAnimate(level));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimate());
    }

}
