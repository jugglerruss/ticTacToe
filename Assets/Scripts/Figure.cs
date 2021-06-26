using Photon.Pun;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Rigidbody))]
public abstract class Figure : MonoBehaviour
{
    #region Private Properties
    protected bool _selected;
    protected bool _placed;
    private Rigidbody _RB => GetComponent<Rigidbody>();
    private Animator _animator => GetComponent<Animator>();
    protected Board _board => FindObjectOfType<Board>();
    #endregion

    #region Inpector Variables
    [SerializeField]
    public int PlayerId;
    #endregion

    #region Public Properties
    public static Figure ActiveFigure;
    public int Strength { get; set; }
    public bool isPlaced
    {
        get
        {
            return _placed;
        }
    }
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
        return Player.My.Figures.Contains(this) && Player.My.IsMyTurn && !_selected && !_placed;
    }
    public void BeginDrag()
    {
        DropAll();
        _selected = true;
        ActiveFigure = this;
        transform.position += new Vector3(0, 1);
        _RB.isKinematic = true;
    }

    private void DropAll()
    {
        foreach (Figure figure in FindObjectsOfType<Figure>())
            if (figure != this)
                figure.Drop();
    }

    protected void Drop()
    {
        _selected = false;
        _RB.isKinematic = false;
    }
    protected void EndDrag()
    {
        Drop();
        _board.HideAllPositions();
    }
    public bool CheckOwner(Player player)
    {
        return transform.parent == player.transform;
    }
    protected abstract void MoveUp();

    #endregion

    #region Public methods
    public void PlaceInPosition(Vector3 cellPosition)
    {
        transform.position = cellPosition + new Vector3(0, 2);
        _placed = true;
        ActiveFigure = null;
        EndDrag();
    }
    public abstract void Deactivate();
    #endregion

    #region Animation
    public static void StartWinAnimation(Player player)
    {
        foreach (Figure figure in player.Figures)
            figure._animator.SetBool("isWin", true);
    }
    public static void StartLoseAnimation(Player player)
    {
        foreach (Figure figure in player.Figures)
            figure._animator.SetBool("isLose", true);
    }
    #endregion


}
