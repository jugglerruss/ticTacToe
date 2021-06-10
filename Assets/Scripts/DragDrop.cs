using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class DragDrop : MonoBehaviour
{
    #region Private Properties
    private bool selected;
    private bool placed;
    Rigidbody RB => GetComponent<Rigidbody>();
    #endregion

    static DragDrop[] dragDrops;

    #region Inpector Variables
    [SerializeField]
    public int PlayerId;
    #endregion

    public PhotonView photonView { get; private set; }
    public static DragDrop ActiveFigure;

    #region Unity Functions
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        dragDrops = FindObjectsOfType<DragDrop>(); 
    }
    private void OnMouseDown()
    {
        Debug.LogError(Player.MyPlayer.isMyTurn);
        if (photonView.IsMine && Player.MyPlayer.isMyTurn && !selected && !placed)
            photonView.RPC(
                   "RPC_UpdateFigure",
                   RpcTarget.All,
                   !selected);
    }

    #endregion
    #region User Interface
    public static DragDrop[] GetAll()
    {
        return dragDrops;
    }
    public void BeginDrag()
    {
        ActiveFigure = this;
        foreach (DragDrop dd in dragDrops)
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
        }
        if (selectedReceived != selected && !selectedReceived)
        {
            EndDrag();
        }
    }

    #endregion
}
