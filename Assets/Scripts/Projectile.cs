using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public float lifetime;
    public int speed;
    public bool flipY;

    void Start()
    {
        GetComponent<SpriteRenderer>().flipY = flipY;
        Invoke("Destroy", lifetime);
    }

    void FixedUpdate()
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
