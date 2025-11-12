using UnityEngine;
using UnityEngine.Splines;

public interface IPatrol
{
    //public void Patrol(SplineContainer splineContainer,float patrolSpeed = 2.0f);

    public SplineContainer GetSplineContainer();
    public float GetPatrolSpeed();
}
