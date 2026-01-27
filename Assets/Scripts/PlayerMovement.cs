using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = movementSpeed * 10 * transform.localScale * Time.deltaTime * direction;
        } 
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
