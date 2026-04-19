using UnityEngine;

public class Scribble : MonoBehaviour
{
    private GameObject target;
    public int movementSpeed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        bool flip = (target.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
        }

        GetComponent<Rigidbody2D>().velocity = !target ? Vector2.zero : movementSpeed * Time.deltaTime * (target.transform.position - transform.position).normalized;
    }
}
