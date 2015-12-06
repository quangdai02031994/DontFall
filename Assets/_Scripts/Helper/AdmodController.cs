using UnityEngine;
using System.Collections;

public class AdmodController : MonoBehaviour {

    public static AdmodController Inst;

    private GoogleMobileAdBanner banner;

    void Awake()
    {
        Inst = this;

        if (!GoogleMobileAd.IsInited)
        {
            GoogleMobileAd.Init();
        }
        banner = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.SMART_BANNER);
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
        if (banner.IsLoaded)
        {
            banner.Hide();
        }
    }
    void OnDisable()
    {
        HideBanner();
    }

    public void LoadFullScreen()
    {
        GoogleMobileAd.LoadInterstitialAd();
    }

    public void ShowFullScreen()
    {
        GoogleMobileAd.ShowInterstitialAd();
    }



}

