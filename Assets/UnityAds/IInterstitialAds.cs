using UnityEngine;
using UnityEngine.Advertisements;

public class IInterstitialAds : MonoBehaviour , IUnityAdsLoadListener , IUnityAdsShowListener 
{
    [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnitId = "Interstitial_iOS";

    private string _adUnitId = null;

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;  //for testing in editor
#endif
    }

    public void LoadIntersitialAds()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowIntersitialAds()
    {
        Advertisement.Show(_adUnitId,this);
        LoadIntersitialAds();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial ad loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load interstitial ad {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Failed to show interstitial ad {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        AdsManager.Instace.isAdShowing = true;
        Debug.Log("Interstitial ad started: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
      Debug.Log("Interstitial ad clicked: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
      Debug.Log("Interstitial ad completed: " + placementId + " - " + showCompletionState.ToString());
        AdsManager.Instace.isAdShowing = false;
        AdsManager.Instace.bannerAds.ShowBannerAds();
    }
}
