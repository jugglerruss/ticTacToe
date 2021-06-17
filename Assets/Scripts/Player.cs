using UnityEngine;

public abstract class Player : MonoBehaviour
{

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    protected Transform _figurePrefab;

    public bool IsMyTurn { get; protected set; }
    public Figure[] Figures { get; protected set; }

    public static Player My;
    protected abstract void Start();
    public void Victory()
    {
        My.IsMyTurn = false;
        Figure.StartWinAnimation();
    }
    public void Lose()
    {
        My.IsMyTurn = false;
        Figure.StartLoseAnimation();
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }
    protected abstract GameObject[] InstantiateFigures();

}
