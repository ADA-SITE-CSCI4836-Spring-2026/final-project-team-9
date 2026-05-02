using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    [Header("Patrol Setup")]
    public float speed = 4f;
    public Transform leftPoint;
    public Transform rightPoint;
    private Transform targetPoint;

    [Header("Audio (Optional)")]
    public AudioManager audioManager;

    void Start()
    {
        // Start by moving towards the right point
        targetPoint = rightPoint;
    }

    void Update()
    {
        // 1. Move back and forth
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if we reached the patrol point
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Swap directions
            targetPoint = (targetPoint == rightPoint) ? leftPoint : rightPoint;
            
            // Flip the sprite left/right
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CatchPlayer();
        }
    }

    // This handles if the player physically bumps into the Zombie's actual collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CatchPlayer();
        }
    }

    void CatchPlayer()
    {
        // Use your Singleton GameManager to instantly trigger the lose state
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }

        // Play the lose sound just like your Lose.cs script does[cite: 7, 11]
        if (audioManager != null)
        {
            audioManager.OnLose();
        }
    }
}