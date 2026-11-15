using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movementSpeed * Vector2.ClampMagnitude(new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
    }
}
