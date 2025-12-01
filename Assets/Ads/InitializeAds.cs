using AudienceNetwork;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
public class InitializeAds : MonoBehaviour , IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId = "5991650";
    [SerializeField] private string _iOSGameId = "5991651";
    [SerializeField] private bool _testMode = true;

    private string _gameId;


    void Awake()
    {
#if UNITY_IOS
                _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
                _gameId = _androidGameId; //for testing in editor
#endif

        InitializeUnityAds();
        InitializeGoogleAds();
#if !UNITY_EDITOR
        InitializeFacebookAds();
#endif
    }

    private void InitializeUnityAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }
    private void InitializeGoogleAds()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

            Debug.Log("Google Mobile Ads initialization complete.");
        });
    }

    private void InitializeFacebookAds()
    {    
         AudienceNetworkAds.Initialize();
    }

    public void OnInitializationComplete()
    {
       Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
       Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

}
