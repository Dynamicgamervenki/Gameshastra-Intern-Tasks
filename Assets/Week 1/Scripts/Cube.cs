using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private float rotationSpeed = 10.0f;
    [SerializeField]
    private float SpeedIncrement = 0.1f;
    [SerializeField]
    bool Ovverride = false;
    [SerializeField]
    float spinningSpeed = 30.0f;

    Rigidbody rb;
    private bool isGrounded = true;

    private bool isMoving = false;

    public int leveltoLoad = 0;

    public bool IsMoving()
    {
       return isMoving;
    }

    private void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovementUsingOldInputSystem();

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
              Jump();
        }

        if(newMove != Vector3.zero && Ovverride)
        {
            Quaternion targetRotation = Quaternion.LookRotation(newMove);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

    }
    Vector3 newMove = Vector3.zero;

    private void MovementUsingOldInputSystem()
    {
        newMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;  


        if (UiManager.Instance && UiManager.Instance.CountdownOver || Ovverride)
        {
           // transform.Translate(newMove * moveSpeed * Time.deltaTime);
           rb.MovePosition(transform.position + newMove * moveSpeed * Time.deltaTime);
           isMoving = newMove != Vector3.zero;

            if (!Ovverride)
            {
                if(newMove.z != 0 || newMove.x != 0)
                {
                    RotateWhileMoving(newMove);
                    moveSpeed += SpeedIncrement * Time.deltaTime;
                }
            }
        }

    }

    private void Jump()
    {
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0));
      // rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
    }

    public void TestingDead()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
       // Material material = GetComponent<Renderer>().material;

        //if(material)
        //    material.color = Color.red;

        if(UiManager.Instance)
            UiManager.Instance.GameOver();

        CameraMovementt camera= Camera.main.GetComponent<CameraMovementt>();
        if (camera != null)
            camera.ShakeCamera();

        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(leveltoLoad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Trap")
        {
            StartCoroutine(Dead());
        }
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Quaternion reset = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.localRotation, reset, 0.6f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void RotateWhileMoving(Vector3 RotateIn)
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.transform.Rotate(Vector3.right * RotateIn.z * spinningSpeed * Time.deltaTime);

    }


}
