using DG.Tweening.Core.Easing;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Cube player;
    [SerializeField] private AdNetwork adNetwork;
    [SerializeField] private AdType adType;

    private void Awake()
    {
        player = FindFirstObjectByType<Cube>();
        statusLabel.text = $"Network: {adNetwork}, Type: {adType}";
    }

    private void OnEnable()
    {
        if (!player)
            player = GameObject.Find("Player").GetComponent<Cube>();

        player.PlayerDead += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        AdsManager.Instace.bannerAds.HideBannerAd();
        PlayAdsBasedOnNetworkAndType();
    }

    private void PlayAdsBasedOnNetworkAndType()
    {
        switch (adNetwork)
        {
            case AdNetwork.UnityAds:
                switch (adType)
                {
                    case AdType.Interstitial:
                        AdsManager.Instace.interstitialAds.ShowIntersitialAds();
                        break;
                    case AdType.Rewarded:
                        AdsManager.Instace.rewardedAds.ShowRewardedAds();
                        break;
                }
                break;

            case AdNetwork.GoogleAds:
                switch (adType)
                {
                    case AdType.Interstitial:
                        AdsManager.Instace.interstitialAds.ShowGoogleIntersitialAds();
                        break;
                    case AdType.Rewarded:
                        AdsManager.Instace.rewardedAds.ShowGoogleRewardedAds();
                        break;
                }
                break;
            case AdNetwork.FacebookAds:
                switch (adType)
                {
                    case AdType.Interstitial:
                        AdsManager.Instace.interstitialAds.ShowFacebookIntersitialAds();
                        break;
                    case AdType.Rewarded:
                        AdsManager.Instace.rewardedAds.ShowFacebookRewardedAds();
                        break;
                }
                break;
        }
    }


    private void OnDisable()
    {
        player.PlayerDead -= OnPlayerDead;
    }


    [SerializeField] private TextMeshProUGUI statusLabel; 
    public void OnNextOptionClicked()
    {
        adNetwork = (AdNetwork)(((int)adNetwork + 1) % System.Enum.GetValues(typeof(AdNetwork)).Length);
        adType = (AdType)(((int)adType + 1) % System.Enum.GetValues(typeof(AdType)).Length);
        if (statusLabel != null)
        {
            statusLabel.text = $"Network: {adNetwork}, Type: {adType}";
        }
        Debug.Log($"Selected Ad Network: {adNetwork}, Ad Type: {adType}");
    }
}

public enum AdNetwork
{
    UnityAds,
    GoogleAds,
    FacebookAds
}

public enum AdType
{
    Interstitial,
    Rewarded,
}

