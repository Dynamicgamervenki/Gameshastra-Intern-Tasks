using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour , IUnityAdsLoadListener , IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitId = "Banner_Android";
    [SerializeField] private string _iOSAdUnitId = "Banner_iOS";


    string AD_UNIT_ID = "ca-app-pub-3940256099942544/6300978111";

    private string _adUnitId = null;

    BannerView bannerView;

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;  //for testing in editor
#endif

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        //Google
        bannerView = new BannerView("AD_UNIT_ID", AdSize.Banner, AdPosition.Bottom);
    }

    #region UnityAds
    public void LoadBannerAds()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_adUnitId, options);
    }
    public void ShowBannerAds()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId,options);
        LoadBannerAds();
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
    #endregion

    #region GoogleAds

    public void LoadGoogleBannerAds()
    {
        bannerView.LoadAd(new AdRequest());
    }

    public void ShowGoogleBannerAds()
    {
        // Implement Google Banner Ad showing logic here
        bannerView.Show();
    }

    #endregion

    #region interfaceMethods
    private void OnBannerShown()
    {
       Debug.Log("Banner ad shown.");
    }

    private void OnBannerHidden()
    {
       Debug.Log("Banner ad hidden.");
    }

    private void OnBannerClicked()
    {
        Debug.Log("Banner ad clicked.");
    }

    private void OnBannerError(string message)
    {
        Debug.LogError("Banner ad error: " + message);
    }

    private void OnBannerLoaded()
    {
      Debug.Log("Banner ad loaded.");
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {
       Debug.Log("Banner ad loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load banner ad {placementId}: {error.ToString()} - {message}");  
    }

    public void OnUnityAdsShowClick(string placementId)
    {
      Debug.Log("Banner ad clicked: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        AdsManager.Instace.isAdShowing = false;
        Debug.Log("Banner ad completed: " + placementId + " - " + showCompletionState.ToString());
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        AdsManager.Instace.isAdShowing = false;
   
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        AdsManager.Instace.isAdShowing = true;
    }
    #endregion
}
