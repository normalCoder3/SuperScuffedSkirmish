using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            gameObject.GetComponent<Rigidbody2D>().velocity = movementSpeed * transform.localScale * 10 * Time.deltaTime * direction;
        } 
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
