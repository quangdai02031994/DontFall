using UnityEngine;
using System.Collections;

public class AdmodController : MonoBehaviour {

    public static AdmodController Inst;

    public GoogleMobileAdBanner banner;

    void Awake()
    {
        Inst = this;

        if (!GoogleMobileAd.IsInited)
        {
            GoogleMobileAd.Init();
        }
        banner = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.SMART_BANNER);
        banner.ShowOnLoad = false;
        DontDestroyOnLoad(transform.gameObject);
        LoadFullScreen();
        GoogleMobileAd.OnInterstitialClosed += OnInterstitialClosed;
    }

    public void ShowBanner()
    {
        if (banner.IsLoaded)
        {
            banner.Show();
        }
    }

    public void HideBanner()
    {
            banner.Hide();
    }
    
    public void LoadFullScreen()
    {
        GoogleMobileAd.LoadInterstitialAd();
    }

    public void ShowFullScreen()
    {
        GoogleMobileAd.ShowInterstitialAd();
    }

    void OnInterstitialClosed()
    {
        LoadFullScreen();
    }

}

