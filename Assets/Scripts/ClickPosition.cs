using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ClickPosition : MonoBehaviourPun
{
    private DragDrop currentFigure;
    private void Start()
    {
    }
    private void OnMouseDown()
    {
        if( Player.MyPlayer.isMyTurn && DragDrop.ActiveFigure != null)
            if (DragDrop.ActiveFigure.photonView.IsMine)
            {
                if (currentFigure != null)
                    if (!CompareWithCurrentFigure(DragDrop.ActiveFigure))
                        return;
                photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, DragDrop.ActiveFigure.photonView.ViewID);
                Player.MyPlayer.RPC_ItsNotMyTurn(true);   
            }        
    }
    public int GetPlayerId()
    {
        if(currentFigure!=null) return currentFigure.PlayerId;
        return 0;
    }
    [PunRPC]
    private void RPC_ClickOnPosition(int activeFigureId)
    {
        var activeFigure = DragDrop.GetAll().Where(f => f.photonView.ViewID == activeFigureId).First();
        if (activeFigure.Placing(transform.position))
        {
            currentFigure = activeFigure;
            if (!Board.CheckWin(currentFigure))
            {
                
            }
        }    
    }
    private bool CompareWithCurrentFigure(DragDrop activeFigure)
    {
        if (activeFigure.transform.localScale.x > currentFigure.transform.localScale.x)
        {
            currentFigure.Deactivate();
            return true;
        }
        Debug.LogError("Текущая фигура меньше размещённой");
        return false;
    }

    
}
