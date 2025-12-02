using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SfxSlider;

    private void Start()
    {
        MasterSlider.onValueChanged.AddListener(SoundMixerManager.Instance.SetMasterVolume);
        MusicSlider.onValueChanged.AddListener(SoundMixerManager.Instance.SetMusicVolume);
        SfxSlider.onValueChanged.AddListener(SoundMixerManager.Instance.SetSfxVolume);
        if (AdsManager.Instace) { AdsManager.Instace.bannerAds.HideBannerAd();}
            
    }

    private void OnDisable()
    {
        MasterSlider.onValueChanged.RemoveListener(SoundMixerManager.Instance.SetMasterVolume);
        MusicSlider.onValueChanged.RemoveListener(SoundMixerManager.Instance.SetMusicVolume);
        SfxSlider.onValueChanged.RemoveListener(SoundMixerManager.Instance.SetSfxVolume);
        if (AdsManager.Instace) { AdsManager.Instace.bannerAds.HideBannerAd();}
            
    }

    public void Resume(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
