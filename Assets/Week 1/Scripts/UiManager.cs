using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI countDownTImer;
    [SerializeField]
    private GameObject DeathPanel;

    public bool CountdownOver = false;

    public static UiManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        countDownTImer.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if(CountdownOver)
        {
            float time = (int)Time.timeSinceLevelLoad - 4;
            score.text = time.ToString();
        }
    }

    public void GameOver()
    {
        DeathPanel.SetActive(true);
        DeathPanel.transform.DOScale(new Vector3(4,4,4),1);
        ResetScore();
    }

    public void ResetScore()
    {
        score.text = "0";
    }

    IEnumerator CountDown()
    {
        for(int i=4; i>0;i--)
        {
            countDownTImer.text = (i-1).ToString();
            // countDownTImer.color = Random.ColorHSV();
            countDownTImer.color = ColorBasedOnCount(i - 1);

            yield return new WaitForSeconds(1);

            if(i == 1)
            {
                CountdownOver = true;
                countDownTImer.gameObject.SetActive(false);
            }
        }
    }

    private Color ColorBasedOnCount(int count)
    {
        switch (count)
        {
            case 0:
                return Color.black;
            case 1:
                return Color.red;
            case 2:
                return Color.blue;
            case 3:
                return Color.green;
        }
        return Color.white;
    }

}
