using System.Collections;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class GameOnline : Game
{
    private PhotonView _photonView => GetComponent<PhotonView>();
    public List<Player> Players => _players.OrderBy(p => (p as PlayerOnline).PhotonView.Owner.ActorNumber).ToList();
    public void SetPlayer(PlayerOnline player)
    {
        _ui.SetMyNickName(PhotonNetwork.NickName);
        _photonView.RPC("RPC_SetEnemyNickName", RpcTarget.Others, PhotonNetwork.NickName);
        player.NoFiguresDraw += Player_NoFiguresDraw;
    }

    [PunRPC]
    private void RPC_SetEnemyNickName(string name)
    {
        _ui.SetEnemyNickName(name);
    }
}
