using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour , IUnityAdsShowListener , IUnityAdsLoadListener
{
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

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

    public void LoadRewardedAds()
    {
       Advertisement.Load(_adUnitId);
    }

    public void ShowRewardedAds()
    {
        Advertisement.Show(_adUnitId,this);
        LoadRewardedAds();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
       Debug.Log("Rewarded ad loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load rewarded ad {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
       Debug.LogError($"Failed to show rewarded ad {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        AdsManager.Instace.isAdShowing = true;
        Debug.Log("Rewarded ad started: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded ad clicked: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        AdsManager.Instace.isAdShowing = false;
        Debug.Log("Rewarded ad completed: " + placementId + " - " + showCompletionState.ToString());
    }
}
