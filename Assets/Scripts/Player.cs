using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Player : MonoBehaviourPun
{

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _figurePrefab;

    public bool IsMyTurn { get; private set; }
    public Figure[] Figures { get; private set; }

    public static Player My;
    private void Start()
    {
        GameObject[] figuresGameObjects;
        if (photonView.IsMine)
        {
            figuresGameObjects = InstantiateFigures();
            Figures = new Figure[figuresGameObjects.Length];
            for (var i = 0; i < figuresGameObjects.Length; i++)
                Figures[i] = figuresGameObjects[i].GetComponent<Figure>();
        }
    }
    public void Victory()
    {
        My.IsMyTurn = false;
        Figure.StartWinAnimation();
        Debug.LogError(PhotonNetwork.NickName + " win");
    }
    public void Lose()
    {
        My.IsMyTurn = false;
        Figure.StartLoseAnimation();
        Debug.LogError(PhotonNetwork.NickName + " lose");
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }
    private GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[6];
        for (var i = 0; i < 6; i++)
        {
            figures[i] = PhotonNetwork.Instantiate(_figurePrefab.name, transform.position, _figurePrefab.transform.rotation);
            photonView.RPC(
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
        My.IsMyTurn = true;
        if (deactivateOthers)
            photonView.RPC("RPC_ItsNotMyTurn", RpcTarget.OthersBuffered, false);
    }
    [PunRPC]
    public void RPC_ItsNotMyTurn(bool activateOthers)
    {
        My.IsMyTurn = false;
        if (activateOthers)
            photonView.RPC("RPC_ItsMyTurn", RpcTarget.OthersBuffered,false);
    }
    #endregion
}
