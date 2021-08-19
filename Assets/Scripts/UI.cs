using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private GameObject _pausePanel;

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }
    public void SetHighScore(int score)
    {
        _highScore.text = score.ToString();
    }
    public void ShowPause(bool show)
    {
        _pausePanel.SetActive(show);
    }
}
