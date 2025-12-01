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

    //Facebook
    private const string Fb_AdId = "VID_HD_9_16_39S_APP_INSTALL#4127762047553490_4127824017547293";
    private AudienceNetwork.RewardedVideoAd rewardedVideoAd;

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
       Advertisement.Load(_adUnitId,this);
    }

    public void ShowRewardedAds()
    {
        Advertisement.Show(_adUnitId,this);
        LoadRewardedAds();
    }

    #region GoogleRewardedAds
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
    #endregion

    #region FacebookRewardedAds

    public void LoadFacebookRewardedAds()
    {
        // Create the rewarded video unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        this.rewardedVideoAd = new AudienceNetwork.RewardedVideoAd(Fb_AdId);

        this.rewardedVideoAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.rewardedVideoAd.RewardedVideoAdDidLoad = (delegate () {
            Debug.Log("RewardedVideo ad loaded.");
        });
        this.rewardedVideoAd.RewardedVideoAdDidFailWithError = (delegate (string error) {
            Debug.Log("RewardedVideo ad failed to load with error: " + error);
        });
        this.rewardedVideoAd.RewardedVideoAdWillLogImpression = (delegate () {
            Debug.Log("RewardedVideo ad logged impression.");
        });
        this.rewardedVideoAd.RewardedVideoAdDidClick = (delegate () {
            Debug.Log("RewardedVideo ad clicked.");
        });

        this.rewardedVideoAd.RewardedVideoAdDidClose = (delegate () {
            Debug.Log("Rewarded video ad did close.");
            AdsManager.Instace.InvokeAdClosed();
            if (this.rewardedVideoAd != null)
            {
                this.rewardedVideoAd.Dispose();
            }
        });

        // Initiate the request to load the ad.
        this.rewardedVideoAd.LoadAd();
    }

    public void ShowFacebookRewardedAds()
    {
        if (this.rewardedVideoAd.IsValid())
        {
            this.rewardedVideoAd.Show();
        }
        else
        {
            Debug.Log("Rewarded video ad not loaded. Please try again later.");
        }
    }

    #endregion

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
