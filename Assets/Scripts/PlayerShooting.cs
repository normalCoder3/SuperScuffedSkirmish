using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Player")]
    public GameObject crosshair;
    public GameObject weapon;
    [Header("Weapon")]
    public GameObject projectile;
    public float projectileSpeed;
    public float projectileLifetime;
    public float attackCooldown;
    private float lastAttackTime;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (crosshair.transform.position - weapon.transform.position).normalized;

        if (mousePosition.x != Input.mousePosition.x || mousePosition.y != Input.mousePosition.y)
        {
            crosshair.transform.position = mousePosition;
            bool flip = (crosshair.transform.position - transform.position).x < 0;

            if (GetComponent<SpriteRenderer>().flipX != flip)
            {
                GetComponent<SpriteRenderer>().flipX = flip;
                weapon.GetComponent<SpriteRenderer>().flipY = flip;
                weapon.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
            }

            weapon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
        }

        if (Input.GetMouseButton(0) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject projectile = Instantiate(this.projectile);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectile.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);
            projectileScript.lifetime = projectileLifetime;

            if (weapon.tag == "Melee")
            {
                weapon.GetComponent<SpriteRenderer>().flipY = true;
                projectile.transform.Translate(new Vector2(aimDirection.x * 0.4f, aimDirection.y * 0.4f), Space.World);
                Invoke("ResetMelee", attackCooldown / 2);
                projectileScript.speed = 0;
            }
            else
            {
                projectile.transform.Translate(new Vector2(aimDirection.x * 0.65f, aimDirection.y * 0.65f), Space.World);
                projectileScript.speed = projectileSpeed;
                projectileScript.flipY = GetComponent<SpriteRenderer>().flipX;
            }
        }
    }
    private void ResetMelee()
    {
        weapon.GetComponent<SpriteRenderer>().flipY = false;
    }
}
