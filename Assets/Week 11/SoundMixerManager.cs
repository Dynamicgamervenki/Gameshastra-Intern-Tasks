using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class SoundMixerManager : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string SfxVolume = "SfxVolume";
    private const string MusicVolume = "MusicVolume";
    public AudioMixer audioMixer;

    public static SoundMixerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(MasterVolume, Mathf.Log10(value) * 20f);
    }
    public void SetSfxVolume(float value)
    {
        audioMixer.SetFloat(SfxVolume, Mathf.Log10(value) * 20f);
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MusicVolume, Mathf.Log10(value) * 20f);
    }

 }

