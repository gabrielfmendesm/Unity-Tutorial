using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController
{
    private static int collectibleCount;
    private static int currentCollected;
    private static int totalCollected;
    private static float timeLeft;
    private static float finalTime;
    private static bool won = false;
    private static float startTime;

    public static void Init()
    {
        collectibleCount = 10;
        currentCollected = 0;
        totalCollected = PlayerPrefs.GetInt("TotalCollected", 0);
        timeLeft = 10f;
        finalTime = 0f;
        won = false;
        startTime = Time.time;
    }

    public static void UpdateTimer()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                finalTime = 10f;
                SceneManager.LoadScene(2);
            }
        }
    }

    public static void CollectibleCollected()
    {
        collectibleCount--;
        currentCollected++;
        totalCollected++;
        PlayerPrefs.SetInt("TotalCollected", totalCollected);
        PlayerPrefs.Save();
        if (collectibleCount <= 0 && timeLeft > 0f)
        {
            won = true;
            finalTime = Time.time - startTime;
            SceneManager.LoadScene(3);
        }
    }

    public static int GetCurrentCollected()
    {
        return currentCollected;
    }

    public static int GetTotalCollected()
    {
        return totalCollected;
    }

    public static float GetTimeLeft()
    {
        return timeLeft;
    }

    public static float GetFinalTime()
    {
        return finalTime;
    }

    public static bool GetWin()
    {
        return won;
    }

    public static void AddTime(float extraTime)
    {
        timeLeft += extraTime;
    }
}