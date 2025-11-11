using System.Diagnostics;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int health = 100;
    protected int stamina = 100;


    public float chaseRange = 10f;
    public float attackRange = 2f;

    //public bool canChase = false;
    //public bool canAttack = false;
    //public bool canPatrol = false;


    public EnemyState currentState = EnemyState.Idle;

    private GameObject Target;

    protected virtual void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        //canChase = Physics.CheckSphere(transform.position, chaseRange,6);
        //canAttack = Physics.CheckSphere(transform.position, attackRange,6);
        //canPatrol = !canChase && !canAttack;

        //if(canPatrol && currentState != EnemyState.Patrol)
        //{
        //    currentState = EnemyState.Patrol;
        //}
        //else if(canChase && !canAttack && currentState != EnemyState.Chase)
        //{
        //        currentState = EnemyState.Chase;
        //}
        //else if(canAttack && currentState != EnemyState.Attack)
        //{
        //    currentState = EnemyState.Attack;
        //}

        HandleEnemyStates();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
