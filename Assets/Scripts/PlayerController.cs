using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed for horizontal movement
    public float jumpForce = 7f;         // Jump force
    public GameObject target;            // Drag the Target (Image 2) GameObject here in the Inspector
    public int nextSceneId;              // Scene ID to load after collision with target

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private bool isGrounded = true;      // To check if the player is on the ground
    private float horizontalInput = 0f; // Current horizontal input direction
    public LayerMask groundLayer;       // Layer mask to detect the ground

    public Transform groundCheck;        // Reference to an empty object that will be used to check if the player is grounded
    public float groundCheckRadius = 0.2f; // The radius of the ground check

    private int jumpCount = 0;           // Counter to track the number of jumps

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        // Check if the player is grounded using raycast or a small overlap circle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // If the player is grounded, reset the jump counter
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Jump if grounded or if the player has a second jump available
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < 1))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0); // Ensure Z rotation is 0
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the assigned target
        if (collision.gameObject == target)
        {
            Debug.Log("Target hit! Changing scene...");
            SceneManager.LoadScene(nextSceneId);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded (this check is now redundant with OverlapCircle)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player has landed on the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Player has left the ground
        }
    }

    // Methods for UI Buttons (Assign these to on-screen buttons)
    public void MoveLeft()
    {
        horizontalInput = -1f;            // Move left
        spriteRenderer.flipX = false;    // Ensure the sprite is not flipped
    }

    public void MoveRight()
    {
        horizontalInput = 1f;            // Move right
        spriteRenderer.flipX = true;     // Flip the sprite on the X-axis
    }

    public void StopMovement()
    {
        horizontalInput = 0f;            // Stop movement when button is released
    }

    public void Jump()
    {
        if (isGrounded || jumpCount < 1)  // Check if grounded or if a jump can be used
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;  // Increment the jump count
        }
    }
}

