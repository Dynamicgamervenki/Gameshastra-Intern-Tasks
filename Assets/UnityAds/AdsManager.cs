using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public IInterstitialAds interstitialAds;
    public RewardedAds rewardedAds;
    public BannerAds bannerAds;
    public bool isAdShowing = false;

    public static AdsManager Instace { get; private set; }

    private void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instace = this;
            DontDestroyOnLoad(this.gameObject);
        }

        interstitialAds.LoadIntersitialAds();
        rewardedAds.LoadRewardedAds();
        bannerAds.LoadBannerAds();
    }

   
}
