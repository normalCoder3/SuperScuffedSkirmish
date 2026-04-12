using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Sprite meleeCrosshair;
    public Sprite rangedCrosshair;


    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x != Input.mousePosition.x || mousePosition.y != Input.mousePosition.y)
        {
            transform.position = mousePosition;
        }
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
