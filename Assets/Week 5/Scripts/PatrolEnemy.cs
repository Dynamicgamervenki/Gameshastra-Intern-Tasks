using UnityEngine;
using UnityEngine.Splines;

public class PatrolEnemy : EnemyBase
{
    public SplinePatrol splinePatrol;

    [SerializeField] private SplineContainer spline;
    [SerializeField] private float patrolSpeed = 2f;

    bool isPatroling = false;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
        splinePatrol.Init(spline, patrolSpeed);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleEnemyStates()
    {
        base.HandleEnemyStates();
        switch (currentState)
        {
            case EnemyState.Patrol :
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        isPatroling = true;
        splinePatrol.MoveAlongSpline();
    }
}
