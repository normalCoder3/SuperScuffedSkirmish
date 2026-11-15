using UnityEngine;

public class DefaultEnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    public GameObject target;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            bool flip = (target.transform.position - transform.position).x < 0;
            if (spriteRenderer.flipX != flip) spriteRenderer.flipX = flip;
            rigidbody.velocity = movementSpeed * (target.transform.position - transform.position).normalized;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
