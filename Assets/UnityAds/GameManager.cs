using DG.Tweening.Core.Easing;
using System;
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
        }
    }


    private void OnDisable()
    {
        player.PlayerDead -= OnPlayerDead;
    }
}

public enum AdNetwork
{
    UnityAds,
    GoogleAds
}

public enum AdType
{
    Interstitial,
    Rewarded,
}

