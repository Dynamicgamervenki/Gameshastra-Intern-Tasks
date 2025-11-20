using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 0.5f;

    #region privateVariables
    private Resource health;
    private Slider healthSlider;
    private Cube player;
    #endregion


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Cube>();
        healthSlider = GetComponent<Slider>();

        health = player.GetHealth();
        health.Changed += UpdateSlider;
        healthSlider.value = health.Max;
    }

    private void AddHealth(float health)
    {
        this.health.Add(health);
        UpdateSlider();
    }

    private void RemoveHealth(float health)
    {
        this.health.Remove(health);
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        StartCoroutine(AnimateSlider());
    }

    IEnumerator AnimateSlider()
    {
        float elapsedTym = 0;
        float start = healthSlider.value;
        float target = health.Current;

        while (elapsedTym < animationSpeed)
        {
            elapsedTym += Time.deltaTime;
            float t = elapsedTym / animationSpeed;
            healthSlider.value = Mathf.Lerp(start,target, Mathf.SmoothStep(0,1,t));
            yield return null;
        }

        healthSlider.value = target;      
    }

}
