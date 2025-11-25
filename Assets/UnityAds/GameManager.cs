using DG.Tweening.Core.Easing;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Cube player;
    private bool flip =false;

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
        AdsManager.Instace.interstitialAds.ShowIntersitialAds();
    }

    private void OnDisable()
    {
        player.PlayerDead -= OnPlayerDead;
    }
}
