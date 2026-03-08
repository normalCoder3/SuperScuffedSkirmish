using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{

    public int speed;
    public int lifetime;
    public bool flipY;

    void Start()
    {
        Invoke("Destroy", lifetime);
        GetComponent<SpriteRenderer>().flipY = flipY;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag); // Work on this
        //if (tag == "PlayerProjectile" ^ collision.tag == "Enemy" || tag != "PlayerProjectile" && collision)
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
