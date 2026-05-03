using UnityEngine;

public class Slash : MonoBehaviour, IProjectile
{

    public float damage;
    public float lifetime;

    void Start()
    {
        if (lifetime > 0)
        {
            Invoke("Destroy", lifetime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != tag.Substring(0, collision.tag.Length))
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    public void ConfigureProjectile(float damage, float lifetime, float speed)
    {
        this.damage = damage;
        this.lifetime = lifetime;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
