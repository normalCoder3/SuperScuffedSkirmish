using UnityEngine;
using UnityEngine.UIElements;

public class Crosshair : MonoBehaviour
{
    public Sprite meleeCrosshair;
    public GameObject player;
    public Sprite rangedCrosshair;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        if (player != null)
        {
            if (spriteRenderer.sprite.name == "meleeCrosshair")
            {
                Vector2 aimDirection = (transform.position - player.transform.position).normalized;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
            }
            else if (transform.rotation.z != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new(Vector3.forward, new Vector3(0f, 0f, transform.position.z));
        if (plane.Raycast(ray, out float distance)) transform.position = ray.GetPoint(distance); ;
    }

    public void SetMelee()
    {
        GetComponent<SpriteRenderer>().sprite = meleeCrosshair;
    }

    public void SetRanged()
    {
        GetComponent<SpriteRenderer>().sprite = rangedCrosshair;
    }
}
