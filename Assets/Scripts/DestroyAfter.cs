using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time;

    void Start()
    {
        Invoke(nameof(Destroy), time);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
