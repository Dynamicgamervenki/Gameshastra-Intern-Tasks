using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class EliteEnemy : EnemyBase,IPatrol,IAttackable,IEnemy
{
   // public SplinePatrol splinePatrol;
    public float attackRange = 2f;
    public bool canAttack = false;

    [SerializeField] private SplineContainer spline;
    [SerializeField] private float patrolSpeed = 2f;

    bool isPatroling = false;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
        //splinePatrol.Init(spline, patrolSpeed);
    }

    protected override void Update()
    {
        base.Update();
        canAttack = Physics.CheckSphere(transform.position, attackRange, playerMask);
        if(canAttack && currentState != EnemyState.Attack)
        {
            currentState = EnemyState.Attack;
        }
    }

    protected override void HandleEnemyStates()
    {
        base.HandleEnemyStates();
        switch (currentState)
        {
            //case EnemyState.Patrol:
            //    Patrol();
            //    break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    //public void Patrol()            //Interface Method
    //{
    //    isPatroling = true;
    //    //splinePatrol.MoveAlongSpline();
    //}

    public void Attack()            //Interface Method
    {
        transform.forward = Vector3.Slerp(transform.forward, target.transform.position, Time.deltaTime * 30);
        target.transform.localScale = new Vector3(0.1f,0.1f, 0.1f);
        target.SendMessage("TestingDead");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public SplineContainer GetSplineContainer()
    {
        return spline;
    }

    public float GetPatrolSpeed()
    {
        return patrolSpeed;
    }

    public void Spawn(Vector3 position)
    {
        Debug.LogWarning("EliteEnemy Spawned");
    }
}
