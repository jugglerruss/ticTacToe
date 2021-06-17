using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviourPun
{
    [SerializeField]
    private Material _selectMaterial;

    private Figure _currentFigure;
    private MeshRenderer _meshRender => GetComponent<MeshRenderer>();
    private Material _defaultMaterial => _meshRender.material;
    private void OnMouseDown()
    {
        if( Player.My.IsMyTurn && Figure.ActiveFigure != null)
            if (Figure.ActiveFigure.photonView.IsMine)
            {
                if (_currentFigure != null)
                    if (!CompareWithCurrentFigure(Figure.ActiveFigure))
                        return;
                photonView.RPC("RPC_ClickOnPosition", RpcTarget.All, Figure.ActiveFigure.photonView.ViewID);
            }        
    }
    public void DoSelect(Figure activeFigure)
    {
        if (_currentFigure==null || activeFigure.Strength > _currentFigure.Strength)
            _meshRender.sharedMaterial.color = new Color(1, 1, 0);
    }
    public void DeSelect()
    {
        _meshRender.sharedMaterial.color = new Color(1, 1, 1);
    }
    public int GetPlayerId()
    {
        if(_currentFigure!=null) return _currentFigure.PlayerId;
        return 0;
    }
    private bool CompareWithCurrentFigure(Figure activeFigure)
    {
        if (activeFigure.Strength > _currentFigure.Strength)
        {
            _currentFigure.Deactivate();
            return true;
        }
        Debug.LogError("Текущая фигура меньше размещённой");
        return false;
    }

    [PunRPC]
    private void RPC_ClickOnPosition(int activeFigureId)
    {
        var activeFigure = PhotonView.Find(activeFigureId).GetComponent<Figure>();
        activeFigure.PlaceInPosition(transform.position);
        _currentFigure = activeFigure;
        if (!Game.Instance.CheckWin(_currentFigure))
        {
            if (_currentFigure.photonView.IsMine)
                Player.My.RPC_ItsNotMyTurn(true);
        }
        
    }

}
