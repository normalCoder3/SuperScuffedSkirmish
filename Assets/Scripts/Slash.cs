using UnityEngine;

public class Slash : MonoBehaviour
{

    public int damage;
    public float lifetime;

    void Start()
    {
        Invoke("Destroy", lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != tag.Substring(0, collision.tag.Length))
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
