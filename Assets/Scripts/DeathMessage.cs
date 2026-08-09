using TMPro;
using UnityEngine;

public class DeathMessage : MonoBehaviour
{
    public string[] messages;

    void Start()
    {
        GetComponent<TMP_Text>().text = messages[Random.Range(0, messages.Length)];
    }
}