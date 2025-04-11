using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public float speed;
    public AudioClip collectibleClip;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
            tempAudioSource.clip = audioSource.clip;
            tempAudioSource.volume = audioSource.volume;
            tempAudioSource.pitch = audioSource.pitch;
            tempAudioSource.loop = false;
            DontDestroyOnLoad(tempAudio);
            tempAudioSource.Play();

            GetComponent<PlayerHealth>().TakeDamage();

            Destroy(tempAudio, tempAudioSource.clip.length);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            AudioSource[] sources = GetComponents<AudioSource>();

            GameObject tempAudio = new GameObject("TempAudioCollectible");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

            tempAudioSource.clip = sources[2].clip;
            tempAudioSource.volume = sources[2].volume;
            tempAudioSource.pitch = sources[2].pitch;
            tempAudioSource.loop = false;
            
            DontDestroyOnLoad(tempAudio);
            
            tempAudioSource.Play();

            GameController.AddTime(2f);
            GameController.CollectibleCollected();

            Destroy(other.gameObject);
            
            Destroy(tempAudio, tempAudioSource.clip.length);
        }
    }
}