using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator),typeof(Rigidbody))]
public class Figure : MonoBehaviourPun
{
    #region Private Properties
    private bool selected;
    private bool placed;
    private Rigidbody RB => GetComponent<Rigidbody>();
    private Animator animator => GetComponent<Animator>();
    #endregion

    #region Inpector Variables
    [SerializeField]
    public int PlayerId;
    #endregion

    #region Public Properties
    public static Figure ActiveFigure;
    public int Strength { get; set; }
    #endregion

    #region Unity Functions
    private void OnMouseDown()
    {
        if (photonView.IsMine && Player.My.IsMyTurn && !selected && !placed)
            photonView.RPC(
                   "RPC_UpdateFigure",
                   RpcTarget.All,
                   !selected);
    }
    #endregion

    #region Private methods
    private void BeginDrag()
    {
        ActiveFigure = this;
        if(photonView.IsMine)
            foreach (Figure figure in Player.My.Figures)
                if (figure != this)
                    figure.EndDrag();
        selected = true;
        transform.position += new Vector3(0, 1);
        RB.isKinematic = true;
    }
    private void EndDrag()
    {
        selected = false;
        RB.isKinematic = false;
        if (photonView.IsMine)
            Board.My.HideAllPositions();
    }

    #endregion

    #region Public methods
    public void PlaceInPosition(Vector3 cellPosition)
    {
        transform.position = cellPosition + new Vector3(0, 2);
        placed = true;
        ActiveFigure = null;
        EndDrag();
    }
    public void Deactivate()
    {
        photonView.RPC("RPC_Deactivate", RpcTarget.All);
    }
    #endregion

    #region Animation
    public static void StartWinAnimation()
    {
        foreach (Figure figure in Player.My.Figures)
            figure.animator.SetBool("isWin", true);
    }
    public static void StartLoseAnimation()
    {
        foreach (Figure figure in Player.My.Figures)
            figure.animator.SetBool("isLose", true);
    }
    #endregion

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
            if (photonView.IsMine)
                Board.My.ShowAvaliblePositions(this);
        }
        if (selectedReceived != selected && !selectedReceived)
        {
            EndDrag();
        }
    }
    #endregion
}
