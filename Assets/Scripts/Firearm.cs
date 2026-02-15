using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Firearm : MonoBehaviour
{
    public GameObject projectile;
    public int projectileSpeed;
    public float firingCooldown;
    private float lastFiringTime;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFiringTime > firingCooldown)
            {
                lastFiringTime = Time.time;
                Fire(GetComponentInParent<PlayerAiming>().FindAimDirection());
            }
        }
    }

    private void Fire(Vector2 aimDirection)
    {
        GameObject projectile = Instantiate(this.projectile);
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.transform.Translate(new Vector2(aimDirection.x * 0.75f, aimDirection.y * 0.75f), Space.World);
        projectile.GetComponent<Projectile>().projectileSpeed = projectileSpeed;
    }
}
