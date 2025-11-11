using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncManager : MonoBehaviour
{

     private Slider loadingSlider;
     private int levelToLoad;

    public void Init(int levelToLoad,Slider loadingSlider)
    {
        this.levelToLoad = levelToLoad;
        this.loadingSlider = loadingSlider;
        LoadLevel();
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(int levelToLoad)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while(!asyncOperation.isDone)
        {
            float PorgressValue = Mathf.Clamp01(asyncOperation.progress/0.9f);
            loadingSlider.value = PorgressValue;
            yield return null;
        }
    }


}
