using System.Diagnostics;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected int health = 100;
    protected int stamina = 100;

    public EnemyState currentState = EnemyState.Idle;

    [HideInInspector]
    public GameObject target;

    public LayerMask playerMask;

    private EnemyController enemyManager;

    //public abstract IEnemy CreateEnemy();

    //public void SpawnEnemy(Vector3 position)
    //{
    //    IEnemy enemy = CreateEnemy();
    //    enemy.Spawn(position);
    //    UnityEngine.Debug.LogWarning("Enemy has been spawned !:)");
    //}

    protected virtual void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyController>();
        enemyManager.AddToEnemyList(this);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        HandleEnemyStates();
    }


    protected virtual void HandleEnemyStates()
    {
        switch (currentState)
        {
            case EnemyState.Idle: 
                Idle();
                break;
        }

    }

    public void Idle()
    {
        UnityEngine.Debug.Log("Enemy is Idle");
    }
}

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Flee
}
