using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitleScreen : MonoBehaviour
{
    public void OnMouseDown()
    {
        SceneManager.LoadScene(0);
    }
}
