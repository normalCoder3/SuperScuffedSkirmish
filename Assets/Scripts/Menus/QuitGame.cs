    using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        new WaitForSeconds(1);
        Application.Quit();
    }
}
