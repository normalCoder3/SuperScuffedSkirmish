using UnityEngine;

public class Pencil : MonoBehaviour, IWeapon
{
    [Header("Slash")]
    public GameObject slash;
    public float damage;
    public float lifetime;
    [Header("Combat")]
    public float attackCooldown;
    private float lastAttackTime;


    void Start()
    {
        GameObject.Find("Crosshair").GetComponent<Crosshair>().SetMelee();
    }

    public void M1(Vector2 aimDirection)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject slash = Instantiate(this.slash, transform.position, transform.rotation);
            slash.transform.Translate(aimDirection, Space.World);
            slash.GetComponent<IProjectile>().Configure(damage, lifetime);
            GetComponent<SpriteRenderer>().flipY = !transform.parent.GetComponent<SpriteRenderer>().flipX;
            Invoke(nameof(ResetMelee), attackCooldown / 2);
        }
    }

    public void M2(Vector2 aimDirection) { }

    private void ResetMelee()
    {
        GetComponent<SpriteRenderer>().flipY = transform.parent.GetComponent<SpriteRenderer>().flipX;
    }
}
