using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int health;
    public int movementSpeed;
    //public ParticleSystem hitEffect;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 ? Vector2.zero 
        : movementSpeed * Time.deltaTime * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyProjectile" || collision.tag == "Enemy")
        {
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Debug.Log(health + " HP left after being hit by a " + collision.name);
        }
    }
}
