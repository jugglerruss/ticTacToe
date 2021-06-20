using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{

    protected const float SCALE_FIGURE = 0.1f;
    public const int COUNT_FIGURES = 6;

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    protected Transform _figurePrefab;    

    public bool IsMyTurn { get; protected set; }
    public Figure[] Figures { get; protected set; }

    public static Player My { get; protected set; }
    protected abstract void Start();
    public void Victory()
    {
        EndGame();
        Figure.StartWinAnimation(this);
    }
    public void Lose()
    {
        EndGame();
        Figure.StartLoseAnimation(this);
    }
    public void Draw()
    {
        EndGame();
    }
    public void EndGame()
    {
        IsMyTurn = false;
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }
    protected abstract GameObject[] InstantiateFigures();

}
