    using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        WaitForSeconds pause = new(1);
        Application.Quit();
    }
}
