using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class Figure : MonoBehaviour
{
    #region Private Properties
    private bool selected;
    private bool placed;
    Rigidbody RB => GetComponent<Rigidbody>();
    #endregion

    static Figure[] figures;

    #region Inpector Variables
    [SerializeField]
    public int PlayerId;
    #endregion

    public PhotonView photonView { get; private set; }
    public static Figure ActiveFigure;
    public int Strength { get; set; }

    #region Unity Functions
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        figures = FindObjectsOfType<Figure>(); 
    }
    private void OnMouseDown()
    {
        if (photonView.IsMine && Player.MyPlayer.isMyTurn && !selected && !placed)
            photonView.RPC(
                   "RPC_UpdateFigure",
                   RpcTarget.All,
                   !selected);
    }

    #endregion
    #region User Interface
    public void BeginDrag()
    {
        ActiveFigure = this;
        foreach (Figure dd in figures)
            if (dd != this)
                dd.EndDrag();
        selected = true;
        transform.position += new Vector3(0, 1);
        RB.isKinematic = true;
    }
    public void EndDrag()
    {
        selected = false;
        RB.isKinematic = false;
        if (photonView.IsMine)
            Board.MyBoard.HideAllPositions();
    }
    public bool Placing(Vector3 cellPosition)
    {
        if (!placed)
        {
            transform.position = cellPosition + new Vector3(0, 2);
            placed = true;
            EndDrag();
            return true;
        }
        return false;
    }
    public void Deactivate()
    {
        photonView.RPC(
                  "RPC_Deactivate",
                  RpcTarget.All);
    }
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
            if(photonView.IsMine)
                Board.MyBoard.ShowAvaliblePositions(this);
        }
        if (selectedReceived != selected && !selectedReceived)
        {
            EndDrag();
        }
    }

    #endregion
}
