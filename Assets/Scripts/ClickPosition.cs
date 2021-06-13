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
            }        
    }
    public void DoSelect(Figure activeFigure)
    {
        if (currentFigure==null || activeFigure.Strength > currentFigure.Strength)
        {
            //meshRender.sharedMaterial = SelectMaterial;
            Color color = meshRender.sharedMaterial.color;
            color = new Color(1,1,0);
            meshRender.sharedMaterial.color = color;
        }
    }
    public void DeSelect()
    {
        //meshRender.sharedMaterial = DefaultMaterial;
        Color color = meshRender.sharedMaterial.color;
        color = new Color(1, 1, 1); 
        meshRender.sharedMaterial.color = color;
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
                if(currentFigure.photonView.IsMine)
                    Player.MyPlayer.RPC_ItsNotMyTurn(true);
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
