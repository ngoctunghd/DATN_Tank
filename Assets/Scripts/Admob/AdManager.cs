using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

    public static AdManager instance;
    [HideInInspector]
    public int bannerId;

    private int countShowAd;

    public int CountShowAd
    {
        get
        {
            return countShowAd;
        }

        set
        {
            countShowAd = value;
        }
    }

    void Awake()
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

    // Use this for initialization
    void Start () {
        countShowAd = 0;

        UM_AdManager.Init();
        bannerId = UM_AdManager.CreateAdBanner(TextAnchor.LowerCenter);

        UM_AdManager.StartInterstitialAd();
        UM_AdManager.LoadInterstitialAd();
    }	


    public void showBanner()
    {        
        UM_AdManager.ShowBanner(bannerId);
    }

    public void HideBanner()
    {
        UM_AdManager.HideBanner(bannerId);
        
    }

    public void RefrehBanner()
    {
        UM_AdManager.RefreshBanner(bannerId);
        
    }

    public void DestroyBanner()
    {
        UM_AdManager.DestroyBanner(bannerId);
    }

    public void ShowInterstitial()
    {
        
        UM_AdManager.ShowInterstitialAd();
    }

    public void LoadInterstitial()
    {
        UM_AdManager.LoadInterstitialAd();
    }

    public void IncreaseCountShowAd()
    {
        countShowAd++;
    }

    public void ResetCountShowAd()
    {
        countShowAd = 0;
    }

    public bool BannerOnScreen()
    {
        return UM_AdManager.IsBannerOnScreen(bannerId);

    }

    public bool BannerIsLoad()
    {
        return UM_AdManager.IsBannerLoaded(bannerId);

    }
}
