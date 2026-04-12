using UnityEngine;

public class Pencil : MonoBehaviour
{
    public GameObject slash;
    public float slashLifetime;
    public float attackCooldown;
    private float lastAttackTime;


    void Start()
    {
        GameObject.Find("Crosshair").GetComponent<Crosshair>().SetMelee();
    }

    public void M1Attack(Vector2 aimDirection)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject projectile = Instantiate(this.slash);
            projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
            projectile.transform.Translate(aimDirection, Space.World);
            GetComponent<SpriteRenderer>().flipY = !GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
            projectile.GetComponent<Projectile>().speed = 0;
            projectile.GetComponent<Projectile>().lifetime = slashLifetime;
            Invoke("ResetMelee", attackCooldown / 2);
        }
    }

    private void ResetMelee()
    {
        GetComponent<SpriteRenderer>().flipY = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX;
    }
}
