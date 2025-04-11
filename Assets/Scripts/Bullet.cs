using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if (!hasHit)
        {
            transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            
            AudioSource originalAudioSource = GetComponent<AudioSource>();
            if (originalAudioSource != null && originalAudioSource.clip != null)
            {
                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
                tempAudioSource.clip = originalAudioSource.clip;
                tempAudioSource.volume = originalAudioSource.volume;
                tempAudioSource.pitch = originalAudioSource.pitch;
                tempAudioSource.loop = false;
                DontDestroyOnLoad(tempAudio);
                tempAudioSource.Play();
                Destroy(tempAudio, tempAudioSource.clip.length);
            }

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            hasHit = true;
            Destroy(gameObject);
        }
    }
}