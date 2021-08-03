using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby: MonoBehaviour
{
    [SerializeField] private Text _highScore;
    [SerializeField] private Dropdown _difficulty;
    [SerializeField] private Camera _camera;
    [Range(0,5)] [SerializeField] private int _quality;
    public string[] Difficulties => new string[3]
    {
        "Beginner",
        "Intermidiate",
        "Hard"
    };
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(_quality);
        Application.targetFrameRate = 60;

        string nickName = PlayerPrefs.GetString("NickName", "Player " + Random.Range(1000, 9999));
        _difficulty.value = PlayerPrefs.GetInt("DifficultyValue", 1);
        SetDifficulty();

        PlayerPrefs.SetInt("CellsRowCount", 9);
        PlayerPrefs.SetInt("CellsColumnCount", 4);
        SetScoreInfo();

    }
    private void SetScoreInfo()
    {

        _highScore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
    private void SetDifficulty()
    {
        PlayerPrefs.SetString("DifficultyName", Difficulties[_difficulty.value]);
        PlayerPrefs.SetInt("DifficultyValue", _difficulty.value);
    }
    public void StartSingle()
    {
        SetDifficulty();
        SceneManager.LoadScene(2);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
