using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int movementSpeed;
    public ParticleSystem hitEffect;

    void FixedUpdate()
    { 
        Vector2 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = GameObject.FindGameObjectWithTag("Player") != null ? movementSpeed * Time.deltaTime * direction : Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}
