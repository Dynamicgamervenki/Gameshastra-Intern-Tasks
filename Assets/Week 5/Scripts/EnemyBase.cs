using System.Diagnostics;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int health = 100;
    protected int stamina = 100;

    public EnemyState currentState = EnemyState.Idle;

    [HideInInspector]
    public GameObject target;

    public LayerMask playerMask;

    protected virtual void Start()
    {
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
