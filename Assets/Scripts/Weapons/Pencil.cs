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

            GameObject slash = Instantiate(this.slash);
            slash.transform.SetPositionAndRotation(transform.position, transform.rotation);
            slash.transform.Translate(aimDirection, Space.World);
            slash.GetComponent<IProjectile>().Configure(damage, lifetime);
            GetComponent<SpriteRenderer>().flipY = !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
            Invoke(nameof(ResetMelee), attackCooldown / 2);
        }
    }

    public void M2(Vector2 aimDirection)
    {
        Debug.Log(name + " has no M2 ability!");
    }

    private void ResetMelee()
    {
        GetComponent<SpriteRenderer>().flipY = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
