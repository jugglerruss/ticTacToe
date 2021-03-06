using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerOnline : Player
{
    public event Action NoFiguresDraw;
    private PhotonView _photonView => GetComponent<PhotonView>();
    public PhotonView PhotonView
    {
        get
        {
            return _photonView;
        }
    }
    public static new PlayerOnline My
    {
        get
        {
            return Player.My as PlayerOnline;
        }
        set
        {
            Player.My = value;
        }
    }
    protected override void Awake()
    {
        (game as GameOnline).SetPlayer(this);
        GameObject[] figuresGameObjects;
        if (_photonView.IsMine)
        {
            figuresGameObjects = InstantiateFigures();
            Figures = new Figure[figuresGameObjects.Length];
            for (var i = 0; i < figuresGameObjects.Length; i++)
                Figures[i] = figuresGameObjects[i].GetComponent<Figure>();
        }
    }
    protected override GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[COUNT_FIGURES];
        for (var i = 0; i < COUNT_FIGURES; i++)
        {
            figures[i] = PhotonNetwork.Instantiate(_figurePrefab.name, transform.localPosition + new Vector3(i * 1.1f, 0, 0), _figurePrefab.transform.rotation);
            _photonView.RPC(
                  "RPC_ChangePositionFigures",
                  RpcTarget.All, figures[i].GetComponent<PhotonView>().ViewID, i);
        }
        return figures;
    }
    #region PunRPC
    [PunRPC]
    public void RPC_ChangePositionFigures(int figureViewID, int i)
    {
        var figure = PhotonView.Find(figureViewID).transform;
        figure.SetParent(transform);
        figure.GetComponent<Figure>().SetStrength(COUNT_FIGURES - i);
    }
    [PunRPC]
    public void RPC_ItsMyTurn(bool deactivateOthers)
    {
        if (My.Figures.Where(f => !f.isPlaced).Count() > 0)
            My.MyTurn(true);
        else
            _photonView.RPC("RPC_DrawAll", RpcTarget.All);
        if (deactivateOthers)
            _photonView.RPC("RPC_ItsNotMyTurn", RpcTarget.OthersBuffered, false);
    }
    [PunRPC]
    public void RPC_ItsNotMyTurn(bool activateOthers)
    {
        My.MyTurn(false);
        if (activateOthers)
            _photonView.RPC("RPC_ItsMyTurn", RpcTarget.OthersBuffered,false);
    }
    [PunRPC]
    public void RPC_DrawAll()
    {
        My.NoFiguresDraw?.Invoke();
    }
    #endregion
}
