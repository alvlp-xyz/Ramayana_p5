using UnityEngine;
using System.Collections;  // Add this line to fix the error

public class AiEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed at which the enemy moves
    public float dodgeChance = 0.2f;     // Chance to dodge when an arrow is approaching
    public float dodgeDistance = 2f;     // Distance to move when dodging
    public float patrolRange = 5f;       // Range within which the enemy moves
    public Transform playerTransform;    // Reference to the player's position
    public float detectionRange = 10f;   // Range at which the enemy can detect arrows

    private Vector3 startPosition;
    private bool isDodging = false;
    private float lastDodgeTime = 0f;
    private float dodgeCooldown = 2f;

    public int health = 50; // AI health

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Patrol within a range
        Patrol();

        // Random chance to dodge if the enemy detects the arrow
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            TryToDodge();
        }
    }

    // Patrol movement (left and right randomly)
    void Patrol()
    {
        if (isDodging || Time.time - lastDodgeTime < dodgeCooldown) return;

        float patrolDirection = Mathf.PingPong(Time.time * moveSpeed, patrolRange);
        transform.position = new Vector3(startPosition.x + patrolDirection - (patrolRange / 2), transform.position.y, transform.position.z);
    }

    // Try to dodge an incoming arrow
    void TryToDodge()
    {
        if (Random.value < dodgeChance && !isDodging)
        {
            StartCoroutine(Dodge());
        }
    }

    // Dodge action
    private IEnumerator Dodge()
    {
        isDodging = true;
        float dodgeTime = 0.3f; // Time for dodge to finish
        Vector3 dodgeDirection = (transform.position.x < playerTransform.position.x) ? Vector3.left : Vector3.right;

        // Move the enemy to dodge the arrow
        Vector3 dodgePosition = transform.position + dodgeDirection * dodgeDistance;
        Vector3 startPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < dodgeTime)
        {
            transform.position = Vector3.Lerp(startPosition, dodgePosition, (elapsedTime / dodgeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait before the next dodge
        lastDodgeTime = Time.time;
        isDodging = false;
    }

    // Handle damage taken by AI
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Handle AI death
    private void Die()
    {
        Debug.Log("AI Enemy Defeated!");
        Destroy(gameObject);  // Destroy the AI object when it dies
    }
}

