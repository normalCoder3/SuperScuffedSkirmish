using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject titleMessage;
    public string[] messages;
    public GameObject scoreText;

    void Start()
    {
        titleMessage.GetComponent<TMP_Text>().text = messages[Random.Range(0, messages.Length)];
        scoreText.GetComponent<TMP_Text>().text = "Score: " + GameData.score;
    }
}
