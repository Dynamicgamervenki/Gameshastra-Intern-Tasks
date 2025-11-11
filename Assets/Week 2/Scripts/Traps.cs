using System.Collections;
using TMPro;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField]
    private Vector3 TargetOffset;
    private void Start()
    {
        StartCoroutine(ActivateTrap());
    }
    IEnumerator ActivateTrap()
    {
        yield return null;
        float time = 5.0f;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            transform.Translate(TargetOffset);
            elapsedTime += Time.deltaTime;
        }
    }
}
