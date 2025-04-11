using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverAndWinActions : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        GameController.Init();
        SceneManager.LoadScene(1);
    }
}