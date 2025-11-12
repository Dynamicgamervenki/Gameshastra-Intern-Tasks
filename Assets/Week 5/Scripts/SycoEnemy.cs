using UnityEngine;
using UnityEngine.AI;

public class SycoEnemy : EnemyBase,IAttackable
{

    public float chaseRange = 10f;
    public float attackRange = 2f;

    public bool canChase = false;
    public bool canAttack = false;

    private NavMeshAgent navMeshAgent;

    public GameObject FireBall;
    public Transform FireBallSpawnPos;
    public float projectileSpeed = 5.0f;

    private bool attackDone = false;

    protected override void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        canAttack = Physics.CheckSphere(transform.position, attackRange, playerMask);
        canChase = Physics.CheckSphere(transform.position, chaseRange, playerMask);

        if (canAttack && currentState != EnemyState.Attack)
        {
           currentState = EnemyState.Attack;
        }
        else if (canChase && !canAttack && currentState != EnemyState.Chase)
        {
           currentState = EnemyState.Chase;
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

    GameObject fire;
    public void Attack()                    //Interface Method
    {
        currentState = EnemyState.Attack;
        Debug.Log("Syco Enemy Attacking!");
        if(!attackDone && target && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            attackDone = true;
            fire = Instantiate(FireBall, FireBallSpawnPos.position * 0.9f, FireBallSpawnPos.rotation,this.transform);
        }
        if(fire)
            fire.transform.position = Vector3.Lerp(fire.transform.position, target.transform.position, projectileSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        currentState = EnemyState.Chase;
        Debug.Log("Syco Enemy Chasing!");
        if(navMeshAgent && target)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
