using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject indicator;
    public GameObject[] enemy;
    private GameObject player;
    public float spawnCooldown;
    private float lastSpawnTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
        {
            Invoke(nameof(Lose), 3);
        } 
        else
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

    private void Lose()
    {
        SceneManager.LoadScene("LossScreen");
    }
}
