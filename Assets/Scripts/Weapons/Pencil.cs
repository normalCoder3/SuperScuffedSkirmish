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
        if (attackCooldown != 0 && lifetime == 0) 
        {
            lifetime = attackCooldown / 2;
            Debug.LogWarning(name + " slash lifetime defaulted to " + lifetime);
        }
        else if (lifetime != 0 && attackCooldown == 0)
        {
            attackCooldown = lifetime * 2;
            Debug.LogWarning(name + " attack cooldown defaulted to " + lifetime);
        }
        GameObject.Find("Crosshair").GetComponent<Crosshair>().SetMelee();
    }

    public void M1(Vector2 aimDirection)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject slash = Instantiate(this.slash);
            slash.transform.SetPositionAndRotation(transform.position, transform.rotation);
            slash.transform.Translate(aimDirection, Space.World);
            GetComponent<SpriteRenderer>().flipY = !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
            slash.GetComponent<IProjectile>().ConfigureProjectile(damage, lifetime, 0);
            Invoke("ResetMelee", attackCooldown / 2);
        }
    }

    public void M2(Vector2 aimDirection)
    {
        Debug.Log(name + " M2 ability does not exist");
    }

    private void ResetMelee()
    {
        GetComponent<SpriteRenderer>().flipY = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
