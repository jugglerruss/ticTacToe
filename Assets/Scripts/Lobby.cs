using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    public Text LogText;
    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void StartSingle()
    {
        Log("StartSingle");
        SceneManager.LoadScene(2);
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void CreateRoom( )
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2, CleanupCacheOnLeave = true });
    }
    public void JoinRoom()
    {
        Log("Find the room");
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
        LogText.text += "\n";
        LogText.text += message;
    }
}