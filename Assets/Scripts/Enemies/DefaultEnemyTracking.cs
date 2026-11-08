using UnityEngine;

public class DefaultEnemyTracking : MonoBehaviour
{
    public float movementSpeed;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public GameObject target;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            bool flip = (target.transform.position - transform.position).x < 0;
            if (spriteRenderer.flipX != flip) spriteRenderer.flipX = flip;
            rb.velocity = movementSpeed * (target.transform.position - transform.position).normalized;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
