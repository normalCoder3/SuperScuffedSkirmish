using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            transform.Translate(movementSpeed * Time.deltaTime * direction);
        } 
    }
}
