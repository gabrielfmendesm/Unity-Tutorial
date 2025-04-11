using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Vector2 initialDirection = Random.insideUnitCircle.normalized;
        rb.linearVelocity = initialDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectedVelocity = Vector2.Reflect(rb.linearVelocity, normal).normalized;
        
        float randomAngle = Random.Range(-10f, 10f);
        reflectedVelocity = Quaternion.Euler(0, 0, randomAngle) * reflectedVelocity;
        
        rb.linearVelocity = reflectedVelocity * speed;
    }
}