using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField] private InterstitialAds _ad;
    [SerializeField] private UI _ui;
    [SerializeField] private BonusScore _bonusScore;
    [SerializeField] private int _tryMaxCount;
    private int _tryCount;
    private List<float> _timeList = new List<float>();
    private int _portalCounter;
    private int _portalCounterMax;
    private int _highScore;
    private Color _colorPortal;
    private ColorChanger _colorChanger;

    public int Score { get; private set; }

    private void Start()
    {
        _tryCount = _tryMaxCount;
        _colorChanger = GetComponent<ColorChanger>();
        _highScore = PlayerPrefs.GetInt("Highscore", 0);
        _ui.SetHighScore(_highScore);
        StartCoroutine(StartSetColor());
        Translator.Instance.UpdateTexts();
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
    public void SetRandomColor()
    {
        var cells = FindObjectsOfType<Cell>();
        _colorPortal = _colorChanger.SetRandomColor(_colorPortal);
        _portalCounter = cells.Where(c => c.Color == _colorPortal && !c.IsClicked).Count();
        _portalCounterMax = _portalCounter;
        AudioManager.Instance.PlayPortal();
    }
    public bool CompareColors(Cell cell)
    {
        var isEqual = cell.Color == _colorPortal;
        if (isEqual)
        {
            AddScore(cell);
        }
        else
        {
            AudioManager.Instance.PlayFailPop();
            Handheld.Vibrate();
            Debug.Log(_tryCount);
            if (_tryCount > 0)
                Pause();
            else
                _ad.ShowAd();

            _tryCount--;
        }
            
        return isEqual;
    }
    public void Continue(bool resetAd)
    {
        if(resetAd)
            _tryCount = _tryMaxCount;
        _ui.SetScore(Score);
        _portalCounter = 0;
        _timeList.Clear();
        Time.timeScale = 1;
        _ui.ShowPause(false);
        SetColor();
    }
    public void LoseScore()
    {
        Score = 0;
    }
    private void Pause()
    {
        Time.timeScale = 0;
        _ui.ShowPause(true);
    }
    private void AddScore(Cell cell)
    {
        float percent = (float)_portalCounter / (float)_portalCounterMax;
        AudioManager.Instance.PlayPop(percent);
        _portalCounter--;
        Score++;
        _ui.SetScore(Score);
        if (_portalCounter == 0 && _portalCounterMax > 3)
        {
            Score += _portalCounterMax;
            _bonusScore.Activate(_portalCounterMax, cell);
        }
        if(Score > _highScore) SetHighScore();
        if (_portalCounter == 0) SetColor();
        CheckAchievements.Instance.CheckAchievementsScore(Score);
        CheckAchievements.Instance.CheckAchievementsPopsInARow(_portalCounterMax - _portalCounter);
        _timeList.Add(Time.fixedTime);
        CheckAchievements.Instance.CheckAchievementsPopsRightInSeconds(_timeList);
    }
    private void SetHighScore()
    {
        _highScore = Score;
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
