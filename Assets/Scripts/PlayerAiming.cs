using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [Header("Game objects")]
    public GameObject crosshair;
    public GameObject gun;
    public GameObject projectilePrefab;

    [Header("Projectile properties")]
    public int projectileSpeed;
    public float firingCooldown;
    private float lastFiringTime;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (crosshair.transform.position - gun.transform.position).normalized;

        if (Input.mousePosition.x != mousePosition.x || Input.mousePosition.y != mousePosition.y)
        {
            crosshair.transform.position = mousePosition;
            bool flipX = (crosshair.transform.position - transform.position).normalized.x < 0;
            if (GetComponent<SpriteRenderer>().flipX != flipX)
            {
                GetComponent<SpriteRenderer>().flipX = flipX;
                gun.transform.position = new Vector2(transform.position.x + (flipX ? -0.25f : 0.25f), transform.position.y - 0.25f);
            }
            gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFiringTime > firingCooldown)
            {
                lastFiringTime = Time.time;
                Fire(aimDirection);
            }
        }
    }

    private void Fire(Vector2 aimDirection)
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.SetPositionAndRotation(gun.transform.position, gun.transform.rotation);
        projectile.transform.Translate(new Vector2(aimDirection.x * 0.75f, aimDirection.y * 0.75f), Space.World);
        projectile.GetComponent<Projectile>().projectileSpeed = projectileSpeed;
    }
}
