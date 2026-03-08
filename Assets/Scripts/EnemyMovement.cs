using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int movementSpeed;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    { 
        Vector2 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        transform.Translate(movementSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
        }
    }
}
