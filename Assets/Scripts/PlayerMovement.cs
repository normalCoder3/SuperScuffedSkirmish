using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 ? Vector2.zero 
        : movementSpeed * Time.deltaTime * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
}
