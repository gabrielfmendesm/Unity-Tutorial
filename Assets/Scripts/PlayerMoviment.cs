using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public float speed;
    public Transform cannon;
    public Transform cannonTip;
    public GameObject bulletPrefab;
    public float shootCooldown = 0.5f;
    private float lastShootTime = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        string[] joysticks = Input.GetJoystickNames();
        bool controllerConnected = false;
        foreach (string j in joysticks)
        {
            if (!string.IsNullOrEmpty(j))
            {
                controllerConnected = true;
                break;
            }
        }
        
        if (controllerConnected)
        {
            float aimH = Input.GetAxis("RightStickHorizontal");
            float aimV = Input.GetAxis("RightStickVertical");
            if (Mathf.Abs(aimH) > 0.2f || Mathf.Abs(aimV) > 0.2f)
            {
                Vector2 aimDir = new Vector2(aimH, aimV);
                float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
                cannon.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - cannon.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            cannon.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        
        if (Input.GetButtonDown("Shoot") && Time.time - lastShootTime >= shootCooldown)
        {
            Vector3 spawnPos = cannonTip.position + cannonTip.right * 0.25f;
            Instantiate(bulletPrefab, spawnPos, cannon.rotation);
            lastShootTime = Time.time;
        }
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