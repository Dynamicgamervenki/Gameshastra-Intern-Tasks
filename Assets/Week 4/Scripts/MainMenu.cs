using Unity.Android.Gradle;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button Btn_Play;
    [SerializeField] private Button Btn_Quit;
    [SerializeField] AsyncManager asyncManager;

    [SerializeField] private int LevelToLoad = 2;
    [SerializeField] private Slider slider;


    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private GameObject LoadingScreen;

    private void Awake()
    {
        Btn_Play.onClick.AddListener(LoadGame);
        Btn_Quit.onClick.AddListener(QuitGame);
    }

    private void LoadGame()
    {
        MainMenuScreen.SetActive(false);
        LoadingScreen.SetActive(true);
        asyncManager.Init(LevelToLoad,slider);
        
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
