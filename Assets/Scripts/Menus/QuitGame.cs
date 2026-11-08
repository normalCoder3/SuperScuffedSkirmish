    using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(Quit), GetComponent<AudioSource>().clip.length);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
