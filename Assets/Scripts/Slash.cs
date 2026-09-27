using UnityEngine;

public class Slash : MonoBehaviour, IProjectile
{
    public float damage;
    public bool flipY;
    public float lifetime;
    public bool persistent;

    void Start()
    {
        if (lifetime > 0)
        {
            Invoke(nameof(Destroy), lifetime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!persistent || tag.Contains(collision.tag) && !tag.Contains("Projectile"))
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    public void Configure(float damage, float lifetime, bool persistent)
    {
        this.damage = damage;
        this.lifetime = lifetime;
        this.persistent = persistent;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void FlipY(bool flip)
    {
        GetComponent<SpriteRenderer>().flipY = flip;
    }

    public float GetDamage()
    {
        return damage;
    }
}
