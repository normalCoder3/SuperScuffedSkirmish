using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifetime;
    public bool flipY;

    void Start()
    {
        Invoke("Destroy", lifetime);
        GetComponent<SpriteRenderer>().flipY = flipY;
    }

    void Update()
    {
        if (speed > 0) { 
        transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != tag.Substring(0, collision.tag.Length)) {
            Destroy(gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
