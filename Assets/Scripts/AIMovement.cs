using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float moveSpeed = 3f;         // Speed for horizontal movement
    public float climbSpeed = 2f;       // Speed for climbing
    public LayerMask groundLayer;       // Layer mask for ground detection
    public Transform groundCheck;       // Transform to check if at the edge of a platform
    public float groundCheckRadius = 0.2f; // Radius for ground check
    public Transform ladderCheck;       // Transform to check for ladders
    public float ladderCheckRadius = 0.2f; // Radius for ladder check

    private Rigidbody2D rb;
    private bool isClimbing = false;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for platform edge or obstacles to turn around
        bool isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isOnGround)
        {
            FlipDirection();
        }

        // Check for ladders
        bool isOnLadder = Physics2D.OverlapCircle(ladderCheck.position, ladderCheckRadius, groundLayer);

        if (isOnLadder)
        {
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            Climb();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void Climb()
    {
        rb.gravityScale = 0; // Disable gravity when climbing
        rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1; // Re-enable gravity
        }
    }
}
