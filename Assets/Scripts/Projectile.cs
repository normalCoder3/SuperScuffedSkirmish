using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Invoke("Destroy", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
