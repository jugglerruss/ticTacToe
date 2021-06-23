using System.Collections;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class GameOnline : Game
{
    private PhotonView _photonView => GetComponent<PhotonView>();
    public void SetPlayer(PlayerOnline player)
    {
        player.NoFiguresDraw += Player_NoFiguresDraw;
    }

    [PunRPC]
    private void RPC_Draw()
    {
       
    }
}
