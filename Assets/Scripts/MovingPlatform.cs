using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float moveDistance = 1.5f;   // how far it moves
    float speed = 2f;          // movement speed

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float direction = movingRight ? 1f : -1f;

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Check bounds
        if (Vector3.Distance(transform.position, startPos) >= moveDistance)
        {
            movingRight = !movingRight;
        }
    }
}