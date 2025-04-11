using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public static LivesUI Instance;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentLives) ? fullHeart : emptyHeart;
        }
    }
}