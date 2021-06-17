using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerOnline : Player
{
    private PhotonView _photonView => GetComponent<PhotonView>();
    protected override void Start()
    {
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
        GameObject[] figures = new GameObject[6];
        for (var i = 0; i < 6; i++)
        {
            figures[i] = PhotonNetwork.Instantiate(_figurePrefab.name, transform.position, _figurePrefab.transform.rotation);
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
        const float SCALE_KOEF = 0.1f;
        var figure = PhotonView.Find(figureViewID).transform;
        figure.SetParent(transform);
        figure.localScale -= new Vector3(SCALE_KOEF, SCALE_KOEF, SCALE_KOEF) * i;
        figure.localPosition = _figurePrefab.transform.localPosition + new Vector3(i * 1.4f - i * 4 * SCALE_KOEF, 0, 0);
        figure.GetComponent<Figure>().Strength = 10 - i;
    }
    [PunRPC]
    public void RPC_ItsMyTurn(bool deactivateOthers)
    {
        (My as PlayerOnline).IsMyTurn = true;
        if (deactivateOthers)
            _photonView.RPC("RPC_ItsNotMyTurn", RpcTarget.OthersBuffered, false);
    }
    [PunRPC]
    public void RPC_ItsNotMyTurn(bool activateOthers)
    {
        (My as PlayerOnline).IsMyTurn = false;
        if (activateOthers)
            _photonView.RPC("RPC_ItsMyTurn", RpcTarget.OthersBuffered,false);
    }
    #endregion
}
