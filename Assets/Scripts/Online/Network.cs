using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameOnline game;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _playerPrefab1;
    [SerializeField] private GameObject _playerPrefab2;

    void Awake()
    {
        GameObject player;
        if (PhotonNetwork.IsMasterClient)
        {
            player = InstantiatePlayer(true);
            PlayerOnline.My = player.GetComponent<PlayerOnline>();
            PlayerOnline.My.MyTurn(true);
        }
        else
        {
            player = InstantiatePlayer(false);
            PlayerOnline.My = player.GetComponent<PlayerOnline>();
        }
        game.SetPlayer(PlayerOnline.My);
        PutCameraToPosition();
    }
    private GameObject InstantiatePlayer(bool isFirst)
    {
        var playerPrefab = isFirst ? _playerPrefab1 : _playerPrefab2;
        return PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }
    private void PutCameraToPosition()
    {
        var cameraPos = Player.My.GetCameraPosition();
        _camera.transform.position = cameraPos.position - Player.My.transform.position;
        _camera.transform.rotation = cameraPos.rotation;
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public void OnReset()
    {
        photonView.RPC("RPC_OnReset", RpcTarget.All); 
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered the room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room", otherPlayer.NickName);
    }

    [PunRPC]
    private void RPC_OnReset()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.SetMasterClient(PhotonNetwork.MasterClient.GetNext());
        PhotonNetwork.LoadLevel(1);
    }
}
