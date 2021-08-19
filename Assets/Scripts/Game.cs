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
        while (_portalCounter == 0 && cells.Length > 0)
        {
            foreach(Color color in _colorChanger.Colors)
            {
                var count = cells.Where(c => c.Color == color && !c.IsClicked).Count();
                if (count > _portalCounter)
                {
                    _portalCounter = count;
                    _colorPortal = color;
                }                    
            }
        }
        _colorChanger.SetColor(_colorPortal);
        _portalCounterMax = _portalCounter;
        AudioManager.Instance.PlayPortal();
    }
    public bool CompareColors(Cell cell)
    {
        var isEqual = cell.Color == _colorPortal;
        if (isEqual)
            AddScore(cell);
        else
            Pause();

        return isEqual;
    }
    private void Pause()
    {
        AudioManager.Instance.PlayFailPop();
        Handheld.Vibrate();
        Time.timeScale = 0;
        _ui.ShowPause(true);
    }
    public void Continue(bool isWatchAds)
    {
        if(!isWatchAds)
            _score = 0;
        _ui.SetScore(_score);
        _portalCounter = 0;
        _timeList.Clear();
        Time.timeScale = 1;
        _ui.ShowPause(false);
        SetColor();
    }
    private void AddScore(Cell cell)
    {
        float percent = (float)_portalCounter / (float)_portalCounterMax;
        AudioManager.Instance.PlayPop(percent);
        _portalCounter--;
        _score++;
        _ui.SetScore(_score);
        if (_portalCounter == 0 && _portalCounterMax > 3)
        {
            _score += _portalCounterMax;
            _bonusScore.Activate(_portalCounterMax, cell);
        }
        if(_score > _highScore) SetHighScore();
        if (_portalCounter == 0) SetColor();
        CheckAchievements.Instance.CheckAchievementsScore(_score);
        CheckAchievements.Instance.CheckAchievementsPopsInARow(_portalCounterMax - _portalCounter);
        _timeList.Add(Time.fixedTime);
        CheckAchievements.Instance.CheckAchievementsPopsRightInSeconds(_timeList);
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
 
}
