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
        if (!_game.CheckWin(_currentFigure))
        {
            if ((_currentFigure as FigureOnline).PhotonView.IsMine)
                PlayerOnline.My.RPC_ItsNotMyTurn(true);
        }        
    }
    protected override void MoveFigureOnPosition()
    {
        _photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, (Figure.ActiveFigure as FigureOnline).PhotonView.ViewID);
    }
}
