using TMPro;
using UnityEngine;

public class LossScreen : MonoBehaviour
{
    public GameObject deathMessage;
    public string[] messages;
    public GameObject scoreText;

    void Start()
    {
        deathMessage.GetComponent<TMP_Text>().text = messages[Random.Range(0, messages.Length)];
        scoreText.GetComponent<TMP_Text>().text = "Score: " + GameData.score;
    }
}