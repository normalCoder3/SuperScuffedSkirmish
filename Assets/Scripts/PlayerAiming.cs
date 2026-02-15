using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject gun;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.mousePosition.x != mousePosition.x || Input.mousePosition.y != mousePosition.y)
        {
            crosshair.transform.position = mousePosition;
            bool flip = (crosshair.transform.position - transform.position).normalized.x < 0;
            if (GetComponent<SpriteRenderer>().flipX != flip)
            {
                GetComponent<SpriteRenderer>().flipX = flip;
                gun.GetComponent<SpriteRenderer>().flipY = flip;
                gun.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
            }
            gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(FindAimDirection().y, FindAimDirection().x));
        }
    }

    public Vector2 FindAimDirection()
    {
        return (crosshair.transform.position - gun.transform.position).normalized;
    }
}
