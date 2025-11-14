using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraMovementt : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private Vector3 Offset;

    private Vector3 ZeroY;


    private void LateUpdate()
    {
        if (Target == null)
            return;

        float Distance = Vector3.Distance(transform.position, Target.transform.position);
        ZeroY = new Vector3(Target.transform.position.x, 0, Target.transform.position.z);
        transform.position = ZeroY + Offset;
    }

    public void ShakeCamera()
    {
        transform.DOShakeRotation(0.2f, new Vector3(0, 0, 5.0f), 10, 40.0f);
    }
}
