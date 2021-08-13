using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class PlayServices : MonoBehaviour
{
    private string _leaderboardID = "CgkIyMm-4-IIEAIQEQ";

    void Start()
    {

        DontDestroyOnLoad(this);
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => { });
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }

    public void AddScoreToLeaderboard(int playerScore)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(playerScore, _leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }
}