using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int projectileSpeed;

    void Start()
    {
        Invoke("Destroy", 1);
    }

    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
