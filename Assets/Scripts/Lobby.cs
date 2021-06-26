using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _logText;
    [SerializeField] private InputField _nickInput;
    [SerializeField] private Dropdown _singleDifficulty;
    public string[] Difficultyes => new string[3]
    {
        "Beginner",
        "Intermidiate",
        "Hard"
    };
    void Start()
    {
        string nickName = PlayerPrefs.GetString("NickName", "Player " + Random.Range(1000, 9999));
        _nickInput.text = nickName;
        _singleDifficulty.value = PlayerPrefs.GetInt("DifficultyValue", 1);
        SetNickName();
        SetDifficulty();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    private void SetDifficulty()
    {
        PlayerPrefs.SetString("DifficultyName", Difficultyes[_singleDifficulty.value]);
        PlayerPrefs.SetInt("DifficultyValue", _singleDifficulty.value);
    }
    private void SetNickName()
    {
        PhotonNetwork.NickName = _nickInput.text;
        PlayerPrefs.SetString("NickName", _nickInput.text);
        Log("Player's name is set to " + PhotonNetwork.NickName);
    }
    public void StartSingle()
    {
        SetDifficulty();
        Log("StartSingle");
        SceneManager.LoadScene(2);
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void CreateRoom( )
    {
        SetNickName();
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2, CleanupCacheOnLeave = true });
    }
    public void JoinRoom()
    {
        Log("Find the room");
        SetNickName();
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            PhotonNetwork.LoadLevel(1);
        else
            StartCoroutine(WaitingForPlayers());

    }
    public override void OnJoinRandomFailed(short returnCode,string messages)
    {
        Log("room not found");
        CreateRoom();
    }
    private IEnumerator WaitingForPlayers()
    {
        Log("Waiting for players...");
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers);
        PhotonNetwork.LoadLevel(1);
    }
    private void Log(string message)
    {
        Debug.Log(message);
        _logText.text += "\n";
        _logText.text += message;
    }
}
