using Photon.Pun;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Rigidbody))]
public abstract class Figure : MonoBehaviour
{
    #region Private Properties
    protected bool selected;
    protected bool placed;
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
    protected void OnMouseDown()
    {
        if (CanTake())
            MoveUp();
    }
    #endregion

    #region protected methods
    protected bool CanTake()
    {
        return Player.My.Figures.Contains(this) && Player.My.IsMyTurn && !selected && !placed;
    }
    protected void BeginDrag()
    {
        foreach (Figure figure in Player.My.Figures)
            if (figure != this)
                figure.EndDrag();
        selected = true;
        ActiveFigure = this;
        transform.position += new Vector3(0, 1);
        RB.isKinematic = true;
    }
    protected void EndDrag()
    {
        selected = false;
        RB.isKinematic = false;
        Board.My.HideAllPositions();
    }
    protected abstract void MoveUp();

    #endregion

    #region Public methods
    public void PlaceInPosition(Vector3 cellPosition)
    {
        transform.position = cellPosition + new Vector3(0, 2);
        placed = true;
        ActiveFigure = null;
        EndDrag();
    }
    public abstract void Deactivate();
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


}
