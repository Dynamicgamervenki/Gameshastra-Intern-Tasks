using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class BossEnemy : EnemyBase, IAttackable, IPatrol
{
    [Header("Patrolling")]
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float patrolSpeed = 6.0f;

    [Header("Enemy States")]
    [SerializeField] private bool canChase = false;
    [SerializeField] private bool canAttack = false;

    [Header("Attack && Chase")]
    [SerializeField] private float attackRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackSpeed = 20.0f;
    [SerializeField] private float HitForce = 10.0f;

    public bool IsPatroling { get; set; }

    #region PrivateVariables
    private NavMeshAgent navMeshAgent;
    #endregion

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();

        if (currentState == EnemyState.Relax) return;

        canAttack = Physics.CheckSphere(transform.position, attackRange, playerMask);
        canChase = Physics.CheckSphere(transform.position, chaseRange, playerMask) && !canAttack;
        IsPatroling = !canAttack && !canChase;

        if (canAttack && currentState != EnemyState.Attack )
        {
            currentState = EnemyState.Attack;
        }
        else if (canChase && !canAttack && currentState != EnemyState.Chase)
        {
            currentState = EnemyState.Chase;
        }
        else if(IsPatroling && !canChase && currentState != EnemyState.Patrol)
        {
            currentState = EnemyState.Patrol;
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }

    protected override void HandleEnemyStates()
    {
        base.HandleEnemyStates();
        switch (currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    private void Chase()
    {
        Debug.Log("Boss Enemy Chase's");
        if (navMeshAgent && target)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    public void Attack()
    {
        DashPlayer();
    }

    public float GetPatrolSpeed()
    {
        return patrolSpeed;
    }

    public SplineContainer GetSplineContainer()
    {
        return spline;
    }

    private void DashPlayer()
    {
        navMeshAgent.SetDestination(target.transform.position);
        navMeshAgent.speed = attackSpeed;
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            StartCoroutine(PushBackTarget());
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    IEnumerator PushBackTarget()
    {
        yield return null;
        if(target.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            target.SendMessage("TestingDead");
            rb.AddForce(transform.forward * HitForce, ForceMode.Impulse);
            currentState = EnemyState.Relax;
        }
    }
}
