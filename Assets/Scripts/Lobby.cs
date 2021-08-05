using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private Camera _camera;
    [Range(0,5)] [SerializeField] private int _quality;
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(_quality);
        Application.targetFrameRate = 60;

        string nickName = PlayerPrefs.GetString("NickName", "Player " + Random.Range(1000, 9999));
        PlayerPrefs.SetInt("CellsRowCount", 9);
        PlayerPrefs.SetInt("CellsColumnCount", 4);
        SetScoreInfo();

    }
    private void SetScoreInfo()
    {
        _highScore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
    public void StartSingle()
    {
        AudioManager.Instance.PlayUIclick();
        SceneManager.LoadScene(1);
    }
    
    public void MuteMusic(bool mute)
    {
        AudioManager.Instance.PlayUIclick();
        AudioManager.Instance.MuteMusic(mute);
    }

    public void QuitApp()
    {
        AudioManager.Instance.PlayUIclick();
        Application.Quit();
    }
}
