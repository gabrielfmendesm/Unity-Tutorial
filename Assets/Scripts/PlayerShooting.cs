using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public Transform cannonTip;
    public GameObject bulletPrefab;

    [Header("Settings")]
    public float shootCooldown = 0.5f;
    private float lastShootTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time - lastShootTime >= shootCooldown)
            {
                AudioSource[] sources = GetComponents<AudioSource>();
                sources[1].Play();
                Vector3 spawnPos = cannonTip.position + cannonTip.right * 0.25f;
                Instantiate(bulletPrefab, spawnPos, cannonTip.rotation);
                lastShootTime = Time.time;
            }
        }
    }
}