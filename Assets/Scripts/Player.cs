using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    protected const float SCALE_FIGURE = 0.1f;
    public const int COUNT_FIGURES = 6;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] protected Transform _figurePrefab;    
    [SerializeField] protected int _id;

    protected Game game => FindObjectOfType<Game>();

    public int Id  => _id; 
    public bool IsMyTurn { get; protected set; }
    public Figure[] Figures { get; protected set; }

    public static Player My { get; protected set; }
    protected abstract void Awake();
    public virtual void Victory()
    {
        MyTurn(false);
        foreach (Figure figure in Figures)
            figure.StartWinAnimation();
    }
    public virtual void Lose()
    {
        MyTurn(false);
        foreach (Figure figure in Figures)
            figure.StartLoseAnimation();
    }
    public virtual void Draw()
    {
        MyTurn(false);
    }
    public void MyTurn(bool isMy)
    {
        IsMyTurn = isMy;
        foreach (Figure figure in Figures)
            figure.StartMyTurnAnimation(isMy);
    }
    public Transform GetCameraPosition() {
        return _cameraTransform;
    }
    protected abstract GameObject[] InstantiateFigures();

}
