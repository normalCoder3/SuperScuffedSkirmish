using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Player")]
    public GameObject crosshair;
    public GameObject gun;
    [Header("Firearm")]
    public GameObject projectile;
    public int projectileSpeed;
    public int projectileLifetime;
    public float firingCooldown;
    private float lastFiringTime;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (crosshair.transform.position - gun.transform.position).normalized;
        if (mousePosition.x != crosshair.transform.position.x || mousePosition.y != crosshair.transform.position.y)
        {
            crosshair.transform.position = mousePosition;
            bool flip = (crosshair.transform.position - transform.position).x < 0;
            if (GetComponent<SpriteRenderer>().flipX != flip)
            {
                GetComponent<SpriteRenderer>().flipX = flip;
                gun.GetComponent<SpriteRenderer>().flipY = flip;
                gun.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
            }
            gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFiringTime > firingCooldown)
            {
                lastFiringTime = Time.time;
                GameObject projectile = Instantiate(this.projectile);
                projectile.transform.SetPositionAndRotation(gun.transform.position, gun.transform.rotation);
                projectile.transform.Translate(new Vector2(aimDirection.x, aimDirection.y), Space.World);
                projectile.GetComponent<Projectile>().speed = projectileSpeed;
                projectile.GetComponent<Projectile>().lifetime = projectileLifetime;
            }
        }
    }
}
