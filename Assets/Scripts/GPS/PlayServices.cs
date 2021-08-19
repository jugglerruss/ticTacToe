using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class PlayServices : MonoBehaviour
{
    private string _leaderboardID = "CgkIyMm-4-IIEAIQEQ";
    private static PlayServices instance;
    public static PlayServices Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayServices>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(PlayServices).Name;
                    instance = obj.AddComponent<PlayServices>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    void Start()
    {
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => { });
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
        
    }

    public void AddScoreToLeaderboard(int playerScore)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(playerScore, _leaderboardID, success => { });
        }
        Debug.Log("AddScoreToLeaderboard" + playerScore);
    }

    public void ShowLeaderboard()
    {
        AudioManager.Instance.PlayUIclick();
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        Debug.Log("ShowLeaderboard");
    }

    public void ShowAchievements()
    {
        AudioManager.Instance.PlayUIclick();
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        Debug.Log("ShowAchievementsUI");
    }

    public void UnlockAchievement(string achievementID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
        Debug.Log("UnlockAchievement " + achievementID);
    }
    public void Quit()
    {
        PlayGamesPlatform.Instance.SignOut(); 
    }
}