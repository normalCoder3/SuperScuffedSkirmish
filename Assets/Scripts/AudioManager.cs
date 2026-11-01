using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioEntry
    {
        public string name;
        public AudioClip clip;
    }

    private AudioSource source;
    [SerializeField] private List<AudioEntry> audioList = new();
    private readonly Dictionary<string, AudioClip> dictionary = new();


    private void Start()
    {
        foreach (var entry in audioList)
        {
            dictionary[entry.name] = entry.clip;
        }

        source = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.PlayOneShot(dictionary[name]);
    }
}
