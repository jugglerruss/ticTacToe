using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    public delegate void NoFigures();
    protected const float SCALE_FIGURE = 0.1f;
    public const int COUNT_FIGURES = 6;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] protected Transform _figurePrefab;    
    [SerializeField] protected int _id;    

    public int Id  => _id; 
    public bool IsMyTurn { get; protected set; }
    public Figure[] Figures { get; protected set; }

    public static Player My { get; protected set; }
    protected abstract void Awake();
    public void Victory()
    {
        MyTurn(false);
        Figure.StartWinAnimation(this);
    }
    public void Lose()
    {
        MyTurn(false);
        Figure.StartLoseAnimation(this);
    }
    public void Draw()
    {
        MyTurn(false);
    }
    public void MyTurn(bool my)
    {
        IsMyTurn = my;
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }
    protected abstract GameObject[] InstantiateFigures();

}
