using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour , IUnityAdsShowListener , IUnityAdsLoadListener
{
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = null;

    //Google
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
    RewardedAd rewardedAd;

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

    public void ShowGoogleRewardedAds()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");

            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }

    public void LoadGoogleRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;

        }

        RewardedAd.Load(AD_UNIT_ID, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load:" + error?.GetMessage());
                return;
            }

            rewardedAd = ad;
            Debug.Log("Rewarded ad loaded.");

            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.LogError("Rewarded ad closed.");
                AdsManager.Instace.InvokeAdClosed();
                LoadGoogleRewardedAd();
            };
            rewardedAd.OnAdFullScreenContentFailed += (error) =>
            {
                Debug.LogError("Rewarded ad failed to show:" + error.GetMessage());
            };
        });
    }


    #region interfaceMethods
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
        AdsManager.Instace.InvokeAdClosed();
        AdsManager.Instace.isAdShowing = false;
        Debug.Log("Rewarded ad completed: " + placementId + " - " + showCompletionState.ToString());
    }
    #endregion
}
