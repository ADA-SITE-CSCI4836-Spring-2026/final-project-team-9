using UnityEngine;

[System.Serializable]
public class MovementSettings
{
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;
}

[System.Serializable]
public class JumpSettings
{
    public float jumpForce = 8f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] MovementSettings pMovement;
    [SerializeField] JumpSettings jump;
    Rigidbody2D rb;
    public Transform groundCheck;
    public AudioManager audioManager;
    public TimeHealth timeHealth;
    public bool CanMove = true;
    public LayerMask groundLayer;
    float groundCheckRadius = 0.1f;
    float moveInput;
    bool isGrounded;
    bool jumpPressed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void HandleInput()
    {
       moveInput = Input.GetAxisRaw("Horizontal"); 
       if (Input.GetKeyDown(KeyCode.Space) && isGrounded) jumpPressed = true;
    }

    void CheckGround()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(!wasGrounded && isGrounded)
        {
            audioManager.OnLand();
        }
    }

    void HandleMovement()
    {
        float targetSpeed = moveInput * pMovement.moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? pMovement.acceleration : pMovement.deceleration;
        float movement = speedDiff * accelRate;

        rb.velocity = new Vector2(rb.velocity.x + movement * Time.fixedDeltaTime, rb.velocity.y);
    }

    void HandleJump()
    {
        if (jumpPressed && isGrounded)
        {
            jumpPressed = false;
            rb.velocity = new Vector2(rb.velocity.x, jump.jumpForce);
        }

        audioManager.OnJump();
    }
    void ApplyBetterGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jump.fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jump.lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void UpdateTime()
    {
        if (Mathf.Abs(moveInput) > 0.1f || !isGrounded) timeHealth.currentState = TimeHealth.PlayerState.Moving;
        else timeHealth.currentState = TimeHealth.PlayerState.Idle;
    }
    void Update()
    {
        if(!CanMove) return;
        HandleInput();
        CheckGround();
        UpdateTime();
    }

    void FixedUpdate()
    {
        if(!CanMove) return;
        HandleMovement();
        HandleJump();

        ApplyBetterGravity();
    }
}