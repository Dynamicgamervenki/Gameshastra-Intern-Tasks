using UnityEngine;
using UnityEngine.Splines;

public class PatrolEnemy : EnemyBase,IPatrol
{
   // public SplinePatrol splinePatrol;

    [SerializeField] private SplineContainer spline;
    [SerializeField] private float patrolSpeed = 2f;

    bool isPatroling = false;

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
  //      splinePatrol.Init(spline, patrolSpeed);
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
            //case EnemyState.Patrol :
            //    Patrol(spline,patrolSpeed);
            //    break;
        }
    }

    public SplineContainer GetSplineContainer()
    {
        return spline;
    }

    public float GetPatrolSpeed()
    {
        return patrolSpeed;
    }

    //public void Patrol(SplineContainer splineContainer, float patrolSpeed = 2)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void Patrol()
    //{
    //    isPatroling = true;
    //    splinePatrol.MoveAlongSpline();
    //}




}
