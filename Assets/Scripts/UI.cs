using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text WinLoseText;
    [SerializeField] private Button ButtonReset;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }
    public void SetHighScore(int score)
    {
        _highScore.text = score.ToString();
    }
}
