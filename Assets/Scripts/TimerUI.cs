using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        GameController.UpdateTimer();

        float t = GameController.GetTimeLeft();
        if (t < 0f) t = 0f;

        int seconds = Mathf.CeilToInt(t);
        timerText.text = "Time Remaining: " + seconds;
    }
}