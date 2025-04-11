using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}