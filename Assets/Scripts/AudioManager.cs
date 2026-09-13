using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sound effects")]
    public AudioSource destroy;
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource hit3;

    public void PlaySound(string name)
    {
        AudioSource source = null;

        switch (name.ToLower())
        {
            case "destroy":
                source = destroy;
                break;
            case "hit1":
                source = hit1;
                break;
            case "hit2":
                source = hit2;
                break;
            case "hit3":
                source = hit3;
                break;
        }

        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(source.clip);
    }
}
