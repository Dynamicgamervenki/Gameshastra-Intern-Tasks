using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform sycoSpawnPos;
    public Transform eliteSpawnPos;

    private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = new SycoSpawner();
       // Spawn(sycoSpawnPos.position);
    }

    //private void Spawn(Vector3 spawnTransform)
    //{
    //    enemySpawner.SpawnEnemy(spawnTransform);
    //}
}
