using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float horizontalSpeed = 5f;

    [Header("Jump Settings")]
    public bool allowJump = true;
    public float jumpForce = 8f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }
    
    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = forwardSpeed;

        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -horizontalSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = horizontalSpeed;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (!allowJump) return;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void SetJumpEnabled(bool enabled)
    {
        allowJump = enabled;
    }
}
