using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        transform.localScale = enemy.transform.localScale;
        Invoke(nameof(Spawn), 1f);
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(this.enemy, GameObject.Find("Enemies").transform);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
    }
}
