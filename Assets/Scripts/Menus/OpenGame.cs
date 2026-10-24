using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        WaitForSeconds pause = new(1);
        SceneManager.LoadScene(1);
    }
}
