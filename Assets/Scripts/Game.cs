using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] protected UI _ui;
    private int _score = 0;
    private int _highScore;
    private Color _colorPortal;
    private ColorChanger _colorChanger;

    private void Start()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _highScore = PlayerPrefs.GetInt("Highscore", 0);
        _ui.SetHighScore(_highScore);
        SetColor();
    }

    public void SetColor()
    {
        _colorPortal = _colorChanger.SetRandomColor(_colorPortal);
    }

    public bool CompareColors(Color color)
    {
        var isEqual = color == _colorPortal;
        if (isEqual)
        {
            _score++;
            if (_highScore < _score) SetHighScore();
            AudioManager.Instance.PlayPop();
        }
        else
        {
            _score = 0;
            Handheld.Vibrate();
            AudioManager.Instance.PlayFailPop();
        }
        _ui.SetScore(_score);
        SetColor();
        return isEqual;
    }
    private void SetHighScore()
    {
        _ui.SetHighScore(_score);
        PlayerPrefs.SetInt("Highscore", _score);
    }

    public void BackToLobby()
    {
        AudioManager.Instance.PlayUIclick();
        SceneManager.LoadScene(0);
    }
}
