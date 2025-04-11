using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuActions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalPointsText; 

    public void StartGame()
    {   
        GameController.Init();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        if (totalPointsText != null)
        {
            int totalPoints = PlayerPrefs.GetInt("TotalCollected", 0);
            totalPointsText.text = "Total Points: " + totalPoints;
        }
    }
}