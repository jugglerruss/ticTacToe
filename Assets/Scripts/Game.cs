using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField] protected UI _ui;
    [SerializeField] protected BonusScore _bonusScore;
    private List<float> _timeList = new List<float>();
    private int _score;
    private int _portalCounter;
    private int _portalCounterMax;
    private int _highScore;
    private Color _colorPortal;
    private ColorChanger _colorChanger;

    private void Start()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _highScore = PlayerPrefs.GetInt("Highscore", 0);
        _ui.SetHighScore(_highScore);
        StartCoroutine(StartSetColor());
    }


    private IEnumerator StartSetColor()
    {
        var stop = false;
        while (!stop)
        {
            var cells = FindObjectsOfType<Cell>();
            if (cells.Count() > 0)
            {
                SetColor();
                stop = true;
                StopCoroutine(StartSetColor());
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void SetColor()
    {
        var cells = FindObjectsOfType<Cell>();
        _portalCounter = 0;
        while (_portalCounter <= 0 && cells.Length > 0)
        {
            _colorPortal = _colorChanger.SetRandomColor(_colorPortal);
            _portalCounter = cells.Where(c => c.Color == _colorPortal && !c.IsClicked).Count();
        }
        _portalCounterMax = _portalCounter;

        AudioManager.Instance.PlayPortal();
    }

    public bool CompareColors(Cell cell)
    {
        var isEqual = cell.Color == _colorPortal;
        if (isEqual)
            AddScore(cell);
        else
            ZeroScore();

        _ui.SetScore(_score);
        if(_portalCounter == 0) SetColor();
        return isEqual;
    }

    private void ZeroScore()
    {
        _score = 0;
        Handheld.Vibrate();
        AudioManager.Instance.PlayFailPop();
        _portalCounter = 0;
        _timeList.Clear();
    }

    private void AddScore(Cell cell)
    {
        AudioManager.Instance.PlayPop(_portalCounter - _portalCounterMax);
        _portalCounter--;
        _score++;
        if (_portalCounter == 0 && _portalCounterMax > 3)
        {
            _score += _portalCounterMax;
            _bonusScore.Activate(_portalCounterMax, cell);
        }
        if(_score > _highScore) SetHighScore();
        CheckAchievementsScore();
        CheckAchievementsPopsInARow();
        _timeList.Add(Time.fixedTime);
        CheckAchievementsPopsRightInSeconds();
    }

    private void SetHighScore()
    {
        _highScore = _score;
        PlayerPrefs.SetInt("Highscore", _highScore);
        _ui.SetHighScore(_highScore);
        PlayServices.Instance.AddScoreToLeaderboard(_highScore);
    }

    public void BackToLobby()
    {
        AudioManager.Instance.PlayUIclick();
        SceneManager.LoadScene(0);
    }
    private void CheckAchievementsScore()
    {
        string id;
        switch (_score)
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
    private void CheckAchievementsPopsInARow()
    {
        string id;
        switch (_portalCounterMax - _portalCounter)
        {
            case 10:
                id = GPS.achievement_10_pops__1_color;
                break;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
    private void CheckAchievementsPopsRightInSeconds()
    {
        string id = "";
        if (PopsRightInSeconds(15, 5))
            id = GPS.achievement_blow_up_in_5_seconds;
        if (PopsRightInSeconds(30, 10))
            id = GPS.achievement_blow_up_in_10_seconds;
        if(id != "") PlayServices.Instance.UnlockAchievement(id);
    }
    private bool PopsRightInSeconds(int score, int seconds)
    {
        if (_timeList.Count >= score)
        {
            var timeDiff = _timeList.ElementAt(_timeList.Count-1) -
                       _timeList.ElementAt(_timeList.Count - _timeList.Count);
            Debug.Log(timeDiff);
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
