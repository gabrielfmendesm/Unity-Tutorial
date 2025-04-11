using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private SpriteRenderer[] spriteRenderers;

    public float blinkDuration = 1.0f;
    public float blinkInterval = 0.2f;

    private bool isInvincible = false;

    void Start()
    {
        currentLives = maxLives;
        LivesUI.Instance.UpdateLives(currentLives);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;
        LivesUI.Instance.UpdateLives(currentLives);

        if (currentLives > 0)
        {
            StartCoroutine(InvincibilityBlink());
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator InvincibilityBlink()
    {
        isInvincible = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            foreach (SpriteRenderer sr in spriteRenderers)
                sr.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            foreach (SpriteRenderer sr in spriteRenderers)
                sr.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval * 2;
        }

        isInvincible = false;
    }
}