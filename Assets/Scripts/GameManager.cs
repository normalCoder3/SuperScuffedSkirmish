using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject[] enemy;
    private GameObject player;
    public float spawnCooldown;
    private float lastSpawnTime;

    void Start()
    {
        Debug.Log("Implement death screen");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnCooldown && player.activeInHierarchy)
        {
            lastSpawnTime = Time.time;
            
            GameObject indicator = Instantiate(this.indicator, GameObject.Find("Enemies").transform);
            indicator.transform.position = new(Random.Range(-6.5f, 11.5f), 24f);

            int index = 0;
            for (int i = 0; i < enemy.Length - 1; i++)
            {
                if (Random.Range(1, 3) == 1)
                {
                    break;
                }
                else
                {
                    index++;
                }
            }
            indicator.GetComponent<Indicator>().enemy = enemy[index];
        }
    }
}
