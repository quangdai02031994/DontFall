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
    }
    void Start()
    {
        banner = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.SMART_BANNER);
        banner.ShowOnLoad = false;
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

}

