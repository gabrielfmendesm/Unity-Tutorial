using UnityEngine;
using TMPro;

public class ShowFinalTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeText;

    void Start()
    {
        float ft = GameController.GetFinalTime();
        if (GameController.GetWin())
        {
            finalTimeText.text = "You took " + ft.ToString("F2") + " seconds!";
        }
        else if (ft == 0f)
        {
            finalTimeText.text = "You died!";
        }
        else
        {
            finalTimeText.text = "Time is up!";
        }
    }
}