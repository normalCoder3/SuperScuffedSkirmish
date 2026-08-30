using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        SceneManager.LoadScene(1);
    }
}
