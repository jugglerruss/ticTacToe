using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private GameObject PlayerPrefab1;
    [SerializeField]
    private GameObject PlayerPrefab2;

    private bool firstPlayer;
    void Start()
    {
        GameObject player;
        if (PhotonNetwork.IsMasterClient)
        {
            player = InstantiatePlayer();
            Player.MyPlayer = player.GetComponent<Player>();
            Player.MyPlayer.RPC_ItsMyTurn(false);
        }
        else
        {
            player = InstantiatePlayer(false);
            Player.MyPlayer = player.GetComponent<Player>();
        }
        PutTheCamera(player);
    }
    private GameObject InstantiatePlayer(bool isFirst = true)
    {
        var playerPrefab = isFirst ? PlayerPrefab1 : PlayerPrefab2;
        return PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
    public void PutTheCamera(GameObject player)
    {
        var cameraPos = player.GetComponent<Player>().GetCameraPosition();
        Camera.transform.position = cameraPos.position - player.transform.position;
        Camera.transform.rotation = cameraPos.rotation;
    }
  
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered the room" , newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room" , otherPlayer.NickName);
    }
}
