using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Game : MonoBehaviour
{
    [SerializeField] protected UI _ui;
    [SerializeField] protected BonusScore _bonusScore;
    private int _score = 0;
    private int _portalCounter = 0;
    private int _portalCounterMax = 0;
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
    }


    public void BackToLobby()
    {
        AudioManager.Instance.PlayUIclick();
        SceneManager.LoadScene(0);
    }
}
