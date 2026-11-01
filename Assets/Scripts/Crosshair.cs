using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Sprite meleeCrosshair;
    public Sprite rangedCrosshair;


    void Start()
    {
        Cursor.visible = false;
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
