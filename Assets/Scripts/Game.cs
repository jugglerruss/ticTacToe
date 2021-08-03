using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

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
        _colorPortal = _colorChanger.SetRandomColor();
    }
    public void OnCellClick(Color color)
    {
        if(color == _colorPortal)
        {
            _score++;
            if (_highScore < _score) SetHighScore();
            AudioManager.Instance.PlayPop();
        }
        else
        {
            _score = 0;
            AudioManager.Instance.PlayFailPop();
        }
        _ui.SetScore(_score);
        _colorPortal = _colorChanger.SetRandomColor();
    }
    private void SetHighScore()
    {
        _ui.SetHighScore(_score);
        PlayerPrefs.SetInt("Highscore", _score);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
