using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    void FixedUpdate()
    {
        Vector2 direction = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
        GetComponent<Rigidbody2D>().velocity = movementSpeed * direction;
    }
}
