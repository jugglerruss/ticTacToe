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
        Debug.LogError(activeFigure.PlayerId);
        var isPlayerOwner = (activeFigure as FigureOnline).PhotonView.IsMine;        
        if(activeFigure.PlaceInPosition(transform.position))
        {
            _currentFigure = activeFigure;
            if (!_game.CheckWin(_currentFigure))
            {
                if (isPlayerOwner)
                    PlayerOnline.My.RPC_ItsNotMyTurn(true);
            }
        }
        else
        {
            if (isPlayerOwner)
                PlayerOnline.My.RPC_ItsNotMyTurn(true);
        }
        
    }
    protected override void MoveFigureOnPosition()
    {
        _photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, (Figure.ActiveFigure as FigureOnline).PhotonView.ViewID);
    }
}
