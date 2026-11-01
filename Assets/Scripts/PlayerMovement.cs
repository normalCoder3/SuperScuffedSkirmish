using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movementSpeed * Vector2.ClampMagnitude(new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
    }
}
