using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class CellOnline : Cell
{
    private PhotonView _photonView => GetComponent<PhotonView>();  

    [PunRPC]
    private void RPC_ClickOnPosition(int activeFigureId)
    {
        var activeFigure = PhotonView.Find(activeFigureId).GetComponent<Figure>();
        activeFigure.PlaceInPosition(transform.position);
        _currentFigure = activeFigure;
        if (!Game.Instance.CheckWin(_currentFigure))
        {
            if ((_currentFigure as FigureOnline).PhotonView.IsMine)
                (Player.My as PlayerOnline).RPC_ItsNotMyTurn(true);
        }        
    }
    protected override void MoveFigureOnPosition()
    {
        _photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, (Figure.ActiveFigure as FigureOnline).PhotonView.ViewID);
    }
}
