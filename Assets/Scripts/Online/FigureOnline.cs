using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class FigureOnline : Figure
{
    private PhotonView _photonView => GetComponent<PhotonView>();
    public PhotonView PhotonView 
    { 
        get {
            return _photonView;
        }
    } 
    protected override void MoveUp()
    {
        _photonView.RPC(
                   "RPC_UpdateFigure",
                   RpcTarget.All,
                   !selected);
    }
    public override void Deactivate()
    {
        _photonView.RPC("RPC_Deactivate", RpcTarget.All);
    }

    #region PunRPC
    [PunRPC]
    public void RPC_Deactivate()
    {
        gameObject.SetActive(false);
    }
    [PunRPC]
    private void RPC_UpdateFigure(bool selectedReceived)
    {
        if (selectedReceived != selected && selectedReceived)
        {
            BeginDrag();
            if (_photonView.IsMine)
                Board.My.ShowAvaliblePositions(this);
        }
    }
    #endregion
}
