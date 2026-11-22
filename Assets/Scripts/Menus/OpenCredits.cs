   using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCredits : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadCredits), GetComponent<AudioSource>().clip.length);
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
