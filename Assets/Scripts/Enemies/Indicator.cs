using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        Invoke(nameof(Spawn), 1f);
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(this.enemy, GameObject.Find("Enemies").transform);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
    }
}
