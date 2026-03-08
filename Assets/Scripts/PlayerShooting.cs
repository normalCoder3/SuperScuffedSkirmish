using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Player")]
    public GameObject crosshair;
    public GameObject weapon;
    [Header("Weapon")]
    public GameObject projectile;
    public int projectileSpeed;
    public int projectileLifetime;
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
            if (weapon.tag == "Ranged")
            {
                lastAttackTime = Time.time;

                GameObject projectile = Instantiate(this.projectile);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectile.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);
                projectile.transform.Translate(new Vector2(aimDirection.x, aimDirection.y), Space.World);
                projectileScript.speed = projectileSpeed;
                projectileScript.lifetime = projectileLifetime;
                projectileScript.flipY = GetComponent<SpriteRenderer>().flipX;
            }
        }
    }
}
