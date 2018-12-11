using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

    private const string BANNER_AD_ID = "ca-app-pub-8204720850752206/1184735970";
    private const string INTERSTITIAL_AD_ID = "ca-app-pub-8204720850752206/3998601575";

    
    private AdMobPlugin admob;
    public bool adsEnabled = true;
    public bool hidden = true;
    int deathCount;
    public int numDeathsBeforeAd;

    private static AdManager _instance;
    public static AdManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AdManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }

    }

    void Start()
    {
        
        deathCount = 0;
        admob = GetComponent<AdMobPlugin>();
        admob.CreateBanner(BANNER_AD_ID, AdMobPlugin.AdSize.SMART_BANNER, false, INTERSTITIAL_AD_ID);
        admob.RequestAd();
        admob.HideBanner();


    }
	
    public void IncrementDeaths()
    {
        deathCount++;
        if(deathCount>0&&deathCount % numDeathsBeforeAd ==0)
        {
            ShowInterstitial();
        }
    }
    
    public void ShowBanner()
    {
        if (adsEnabled && hidden)
        {
            admob.ShowBanner();
            hidden = false;
        }
    }
    public void HideBanner()
    {
        if (adsEnabled && !hidden)
        {
            admob.HideBanner();
            hidden = true;
        }
    }

    public void ShowInterstitial()
    {
        if (adsEnabled)
        {
            admob.RequestInterstitial();
        }
    }
    void OnEnable()
    {
        AdMobPlugin.InterstitialLoaded += HandleInterstitialLoaded;
    }
    void OnDisable()
    {
        AdMobPlugin.InterstitialLoaded -= HandleInterstitialLoaded;
    }
    void HandleInterstitialLoaded()
    {
        admob.ShowInterstitial();
    }

    public void DisableAds()
    {
        Debug.Log("Ads Disabled");
        adsEnabled = false;
        hidden = true;
        admob.HideBanner();
    }
}
