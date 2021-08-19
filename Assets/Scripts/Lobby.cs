using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby: MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LobbyUI _ui;
    [Range(0,5)] [SerializeField] private int _quality;
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(_quality);
        Application.targetFrameRate = 60;

        var mute = PlayerPrefs.GetInt("mute", 0) == 1;
        AudioManager.Instance.MuteMusic(mute);
        _ui.ShowHideMuteButtons(mute);

        PlayerPrefs.SetInt("CellsRowCount", 9);
        PlayerPrefs.SetInt("CellsColumnCount", 4);
        _ui.SetScoreInfo();

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
        PlayServices.Instance.Quit();
        Application.Quit();
    }
}
