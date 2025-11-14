using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 5.0f;
    [SerializeField] private float LifeSpan = 5.0f;

    private void OnEnable()
    {
        Invoke("SetLifeSpan", LifeSpan);
    }

    private void Update()
    {
        transform.Translate(-transform.right * BulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }

    private void SetLifeSpan()
    {
        gameObject.SetActive(false);
    }

}
