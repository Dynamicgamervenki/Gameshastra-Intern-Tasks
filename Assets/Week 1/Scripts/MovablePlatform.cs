using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MovablePlatform : MonoBehaviour
{
    private Vector3 TargetPosition;

    [Header("Properties")]
    [SerializeField]
    bool isMoveable = true;
    [SerializeField]
    private float movingSpeed = 0.5f;

    [SerializeField]
    bool isRotatable = true;
    [SerializeField]
    bool RotateInfinite = false;
    [SerializeField]
    float rotateSpeed = 0.6f;

    [SerializeField]
    bool isScalable = true;
    [SerializeField]
    private float scaleAmplitude = 1f;
    [SerializeField]
    private float scaleSpeed = 2f;
    [SerializeField]
    private Axis axisToScale;
    


    [Header("References")]
    [SerializeField]
    private Transform posA;
    [SerializeField]
    private Transform posB;

    private Vector3 initialScale;

    public bool canMoveAlongPlatform = false;



    public void Init(bool isScalable,bool isMoveable, Axis axisToScale,float scaleSpeed,float scaleSize)
    {
        this.isScalable = isScalable;
        this.isMoveable = isMoveable;
        this.axisToScale = axisToScale;
        this.scaleSpeed = scaleSpeed;
        this.scaleAmplitude = scaleSize;
    }


    private void Start()
    {
        if (isMoveable)
        {
            if (Vector3.Distance(transform.position, posA.position) < Vector3.Distance(transform.position, posB.position))
            {
                TargetPosition = posB.position;
            }
            else
            {
                TargetPosition = posA.position;
            }

        }
            if (axisToScale == Axis.y)
            {
                scaleAmplitude = Random.Range(1, 6);
                scaleSpeed = Random.Range(0, 15);
            }
            else
            {
                scaleSpeed = Random.Range(0, 10);
                scaleAmplitude = Random.Range(0.3f, 1.5f);
            } 

        initialScale = transform.localScale;

    }

    private void Update()
    {
        if (isScalable)
        {
            Scale();
        }
        if(isRotatable)
        {
            Rotate();
        }
        if (isMoveable)
        {
            Move();
        }
    }

    private void Move()
    {
         transform.position = Vector3.Lerp(transform.position, TargetPosition,movingSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position,TargetPosition) < 0.5f)
        {
            if (TargetPosition == posA.position)
            {
                TargetPosition = posB.position;
            }
            else
            {
                TargetPosition = posA.position;
            }
        }
    }

    void Scale()
    {
            //Vector3 baseScale = new Vector3(7.6f, 1, 1); 
        
            float sinValue = Mathf.Sin(Time.time * scaleSpeed);
            float currentScaleFactor = 1f + (sinValue * scaleAmplitude);
            if (axisToScale == Axis.x) 
            { 
                transform.localScale = new Vector3(initialScale.x * Mathf.Abs(currentScaleFactor), initialScale.y , initialScale.z);
            }
            else if(axisToScale == Axis.y)
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y * Mathf.Abs(currentScaleFactor), initialScale.z);
            }
            else
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z * Mathf.Abs(currentScaleFactor));
            }
    }

    void Rotate()
    {
        Quaternion target = Quaternion.Euler(0,180, 0);
        if(RotateInfinite)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canMoveAlongPlatform)
        {
            collision.gameObject.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canMoveAlongPlatform)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }



}

public enum Axis
{
    x,
    y, 
    z
}
