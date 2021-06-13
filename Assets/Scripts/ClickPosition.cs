using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ClickPosition : MonoBehaviourPun
{
    [SerializeField]
    private Material SelectMaterial;

    private Figure currentFigure;
    private MeshRenderer meshRender;
    private Material DefaultMaterial;
    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        DefaultMaterial = meshRender.material;
    }
    private void OnMouseDown()
    {
        if( Player.MyPlayer.isMyTurn && Figure.ActiveFigure != null)
            if (Figure.ActiveFigure.photonView.IsMine)
            {
                if (currentFigure != null)
                    if (!CompareWithCurrentFigure(Figure.ActiveFigure))
                        return;
                photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, Figure.ActiveFigure.photonView.ViewID);
                Player.MyPlayer.RPC_ItsNotMyTurn(true);   
            }        
    }
    public void DoSelect(Figure activeFigure)
    {
        if (currentFigure==null || activeFigure.Strength > currentFigure.Strength)
        {
            meshRender.sharedMaterial = SelectMaterial;
        }
    }
    public void DeSelect()
    {
        Debug.LogError("DeSelect");
        meshRender.sharedMaterial = DefaultMaterial;
    }
    public int GetPlayerId()
    {
        if(currentFigure!=null) return currentFigure.PlayerId;
        return 0;
    }
    [PunRPC]
    private void RPC_ClickOnPosition(int activeFigureId)
    {
        var activeFigure = PhotonView.Find(activeFigureId).GetComponent<Figure>();
        if (activeFigure.Placing(transform.position))
        {
            currentFigure = activeFigure;
            if (!Board.MyBoard.CheckWin(currentFigure))
            {
                
            }
        }    
    }
    private bool CompareWithCurrentFigure(Figure activeFigure)
    {
        if (activeFigure.Strength > currentFigure.Strength)
        {
            currentFigure.Deactivate();
            return true;
        }
        Debug.LogError("Текущая фигура меньше размещённой");
        return false;
    }

    
}
