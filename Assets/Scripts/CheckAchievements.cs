using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckAchievements : MonoBehaviour
{
    private static CheckAchievements instance;
    public static CheckAchievements Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CheckAchievements>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CheckAchievements).Name;
                    instance = obj.AddComponent<CheckAchievements>();
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
    public void CheckAchievementsScore(int score)
    {
        string id;
        switch (score)
        {
            case 100:
                id = GPS.achievement_first_pops;
                break;
            case 300:
                id = GPS.achievement_300_spartanpops;
                break;
            case 666:
                id = GPS.achievement_devil_number;
                break;
            case 1000:
                id = GPS.achievement_legendary;
                break;
            case 9999:
                id = GPS.achievement_godlike;
                break;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
    public void CheckAchievementsPopsInARow(int popCount)
    {
        string id;
        switch (popCount)
        {
            case 10:
                id = GPS.achievement_10_pops__1_color;
                break;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
    public void CheckAchievementsPopsRightInSeconds(List<float> timeList)
    {
        string id = "";
        if (PopsRightInSeconds(timeList,15, 5))
            id = GPS.achievement_blow_up_in_5_seconds;
        if (PopsRightInSeconds(timeList,30, 10))
            id = GPS.achievement_blow_up_in_10_seconds;
        if (id != "") PlayServices.Instance.UnlockAchievement(id);
    }
    public void CheckAchievementsHole(int clickCount)
    {
        string id;
        switch (clickCount)
        {
            case 1:
                id = GPS.achievement_toch_the_hole;
                break;
            case 3:
                id = GPS.achievement_black_hole_or_not_so_black;
                break; ;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
    public void CheckAchievementsLobby(int clickCountLobby)
    {
        string id;
        switch (clickCountLobby)
        {
            case 10:
                id = GPS.achievement_relax;
                break;
            case 100:
                id = GPS.achievement_super_relax;
                break;
            case 1000:
                id = GPS.achievement_mega_relax;
                break;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
    private bool PopsRightInSeconds(List<float> timeList,int score, int seconds)
    {
        if (timeList.Count >= score)
        {
            var timeDiff = timeList.ElementAt(timeList.Count - 1) -
                       timeList.ElementAt(timeList.Count - timeList.Count);
            if (timeDiff <= seconds)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

}
