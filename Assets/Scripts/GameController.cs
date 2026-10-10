using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject indicator;
    public GameObject[] enemy;
    private GameObject player;
    public float spawnCooldown;
    private float lastSpawnTime;
    public int score;
    public TMP_Text scoreText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText.text = "0";
    }

    void Update()
    {
        if (player == null)
        {
            Invoke(nameof(Lose), 1);
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
                    index++;
                }
                indicator.GetComponent<Indicator>().enemy = enemy[index];
            }
        }
    }

    private void Lose()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("LossScreen");
    }

    public void UpdateScore(int change)
    {
        score += change;
        scoreText.text = score.ToString();
    }
}
