using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this for scene management

public class BossFight : MonoBehaviour
{
    public GameObject arrowPrefab;        // Arrow prefab
    public Transform playerTransform;     // Player's position to spawn the arrow
    public Transform targetTransform;     // Target position
    public float arrowSpeed = 10f;        // Speed of the arrow
    public int targetHealth = 100;        // Target's initial health
    public Button shootButton;            // Canvas button to shoot an arrow
    public string sceneToLoad;            // Name of the scene to load when target is defeated

    void Start()
    {
        // Attach the ShootArrow function to the button's click event
        if (shootButton != null)
        {
            shootButton.onClick.AddListener(ShootArrow);
        }
    }

void ShootArrow()
{
    // Instantiate arrow at the player's position
    GameObject arrow = Instantiate(arrowPrefab, playerTransform.position, Quaternion.identity);

    // Add a script to move the arrow forward
    Arrow arrowScript = arrow.AddComponent<Arrow>();
    
    // Pass only the necessary parameters to Initialize method (speed and the BossFight reference)
    arrowScript.Initialize(arrowSpeed, this);
}


    public void ReduceTargetHealth(int damage)
    {
        targetHealth -= damage;
        if (targetHealth <= 0)
        {
            // Handle target defeat (e.g., destroy object, play animation)
            Debug.Log("Target defeated!");
            Destroy(targetTransform.gameObject);

            // Load the next scene when the target is defeated
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}

public class Arrow : MonoBehaviour
{
    private float speed;
    private BossFight bossFight;
    private int damage = 10; // Damage dealt to the target

    // This method is used to initialize the arrow's speed and the reference to the BossFight
    public void Initialize(float arrowSpeed, BossFight boss)
    {
        speed = arrowSpeed;      // Set the speed of the arrow
        bossFight = boss;        // Set the reference to BossFight

        // Flip the arrow on the X-axis so it faces right
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        // Move the arrow along the positive X direction
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Optional: Destroy the arrow when it goes off-screen
        if (transform.position.x > 10f)  // Adjust 10f based on your scene's world bounds
        {
            Destroy(gameObject);
        }
    }

    // Detect collision with the target (non-trigger collision)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the arrow collides with the target
        if (collision.gameObject.CompareTag("Target")) // Make sure your target has the "Target" tag
        {
            // Deal damage to the target
            bossFight.ReduceTargetHealth(damage);

            // Destroy the arrow
            Destroy(gameObject);
        }

        // Check if the arrow collides with the AI enemy
        if (collision.gameObject.CompareTag("AIEnemy"))  // Make sure the enemy has the "AIEnemy" tag
        {
            // Apply damage to AI enemy (add your own damage handling logic)
            AiEnemy aiEnemy = collision.gameObject.GetComponent<AiEnemy>();
            if (aiEnemy != null)
            {
                // Apply damage (you can implement health for the AI)
                aiEnemy.TakeDamage(damage);
            }

            // Destroy the arrow
            Destroy(gameObject);
        }
    }
}

