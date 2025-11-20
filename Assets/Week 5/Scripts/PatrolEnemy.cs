using UnityEngine;
using UnityEngine.Splines;

public class PatrolEnemy : EnemyBase,IPatrol
{

    [SerializeField] private SplineContainer spline;
    [SerializeField] private float patrolSpeed = 2f;

    public bool IsPatroling { get; set; }

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
        IsPatroling = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleEnemyStates()
    {
        base.HandleEnemyStates();
    }

    public SplineContainer GetSplineContainer()
    {
        return spline;
    }

    public float GetPatrolSpeed()
    {
        return patrolSpeed;
    }
}
