using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitleScreen : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        WaitForSeconds pause = new(1);
        SceneManager.LoadScene(0);
    }
}
