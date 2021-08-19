using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{

    [SerializeField] private Button _buttonMute;
    [SerializeField] private Button _buttonUnMute;
    [SerializeField] private TextMeshProUGUI _highScore;
    public void ShowHideMuteButtons(bool mute)
    {
        _buttonMute.gameObject.SetActive(!mute);
        _buttonUnMute.gameObject.SetActive(mute);
    }
    public void SetScoreInfo()
    {
        _highScore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}
