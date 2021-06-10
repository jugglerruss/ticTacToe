using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Player : MonoBehaviourPun
{
    const float SCALE_KOEF = 0.1f;

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _figurePrefab;

    public bool isMyTurn { get; private set; }
    public static Player MyPlayer;
    private void Start()
    {
        if(photonView.IsMine)
            InstantiateFigures();
    }

    private GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[6];
        for(var i=0; i < 6; i++)
        {
            figures[i] = PhotonNetwork.Instantiate(_figurePrefab.name, transform.position, _figurePrefab.transform.rotation);
            photonView.RPC(
                  "RPC_ChangePositionFigures",
                  RpcTarget.All,figures[i].GetComponent<PhotonView>().ViewID, i);
        }
        return figures;
    }
    public static Player[] GetAll()
    {
        return FindObjectsOfType<Player>();
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }

    [PunRPC]
    public void RPC_ChangePositionFigures(int figureViewID, int i)
    {
        var figure = PhotonView.Find(figureViewID).transform;
        figure.SetParent(transform);
        figure.localScale -= new Vector3(SCALE_KOEF, SCALE_KOEF, SCALE_KOEF) * i;
        figure.localPosition = _figurePrefab.transform.localPosition + new Vector3(i - i * 1.5f * SCALE_KOEF, 0, 0);
    }
    [PunRPC]
    public void RPC_ItsMyTurn(bool deactivateOthers)
    {
        MyPlayer.isMyTurn = true;
        if (deactivateOthers)
            photonView.RPC("RPC_ItsNotMyTurn", RpcTarget.OthersBuffered, false);
    }
    [PunRPC]
    public void RPC_ItsNotMyTurn(bool activateOthers)
    {
        MyPlayer.isMyTurn = false;
        if (activateOthers)
            photonView.RPC("RPC_ItsMyTurn", RpcTarget.OthersBuffered,false);
    }
}
