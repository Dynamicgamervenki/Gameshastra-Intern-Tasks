using DG.Tweening;
using Unity.Android.Gradle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player2D : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float jumpForce = 15.0f;


    private bool isGrounded = true;
    private Vector2 movementInput;
    float horizontalAxis;

    InputSystem_Actions inputSystemActions;
    Vector2 moveInput;


    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Enable();
        inputSystemActions.Player.Jump.performed += Jump;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = inputSystemActions.Player.Move.ReadValue<Vector2>();

        //horizontalAxis = Input.GetAxis("Horizontal");
        horizontalAxis = moveInput.x;
        movementInput = new Vector2(horizontalAxis, rb.linearVelocityY).normalized;

        Debug.Log("rb.linearVelocity : " +  rb.linearVelocity);

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //  rb.MovePosition(rb.position + movementInput * moveSpeed * Time.deltaTime);
        // rb.linearVelocity = movementInput * moveSpeed * Time.deltaTime;
        rb.linearVelocity = new Vector2(horizontalAxis * moveSpeed, rb.linearVelocityY);

    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //  rb.AddForce(Vector2.up * jumpForce , ForceMode2D.Force);
        // rb.linearVelocity = Vector2.up * jumpForce * Time.deltaTime;
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (!isGrounded && collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            rb.linearVelocity = new Vector2(rb.linearVelocityX * moveSpeed, 15.0f);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void ReloadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }



}
