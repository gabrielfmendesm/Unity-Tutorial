using UnityEngine;
using TMPro;

public class InGameScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    void Update()
    {
        scoreText.text = "Collected: " + GameController.GetCurrentCollected().ToString();
    }
}