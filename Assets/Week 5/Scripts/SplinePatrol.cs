using UnityEngine;
using UnityEngine.Splines;

public class SplinePatrol : MonoBehaviour
{
    private SplineContainer spline;
    private float patrolSpeed = 2f;

    private float distancePercentage = 0f;
    private Vector3 tangent;
    private float splineLength;

    private void Start()
    {
        if (spline != null)
        {
            splineLength = spline.CalculateLength();
        }
    }

    public void Init(SplineContainer splineContainer,float patrolSpeed = 2f)
    {
        this.spline = splineContainer;
        this.patrolSpeed = patrolSpeed;
    }

    public void MoveAlongSpline()
    {
        if (spline == null) return;

        distancePercentage += patrolSpeed * Time.deltaTime / spline.CalculateLength();
        if (distancePercentage > 1f)
        {
            //  distancePercentage = 0f; // Loop back to start
            distancePercentage = 1f;    
            patrolSpeed = -patrolSpeed;               // reverse direction
        }

        if(distancePercentage <= 0f)
        {
            distancePercentage = 0f;
            patrolSpeed = -patrolSpeed;

        }

        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition;

        tangent = spline.EvaluateTangent(distancePercentage);

        if(patrolSpeed < 0f)
            tangent = -tangent;

        if (tangent != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(tangent);
        }
    }
}
