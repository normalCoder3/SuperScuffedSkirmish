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
        Vector2 aimDirection = (crosshair.transform.position - gameObject.transform.position).normalized;

        if (Input.mousePosition.x != mousePosition.x || Input.mousePosition.y != mousePosition.y)
        {
            crosshair.transform.position = mousePosition;
            if (aimDirection.x > 0 && GetComponent<SpriteRenderer>().flipX || aimDirection.x < 0 && !GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
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
        projectile.transform.Translate(new Vector2(aimDirection.x * 0.85f, aimDirection.y * 0.85f), Space.World);
        projectile.GetComponent<Rigidbody2D>().velocity = aimDirection * projectileSpeed;
    }
}
