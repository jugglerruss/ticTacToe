using Photon.Pun;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator),typeof(Rigidbody))]
public abstract class Figure : MonoBehaviour
{
    #region Private Properties
    protected bool _selected;
    protected bool _placed;
    private Rigidbody _RB => GetComponent<Rigidbody>();
    private Animator _animator => GetComponent<Animator>();
    protected Board _board => FindObjectOfType<Board>();
    private bool _invincinle;
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
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            StartLobbyAnimation();
    }
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
    public void Select()
    {
        DropAll();
        _selected = true;
        ActiveFigure = this;
        //transform.position += new Vector3(0, 1);
        _RB.isKinematic = true;
        SelectAnimation(_selected);
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
        SelectAnimation(_selected);
    }
    protected void Deselect()
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
        transform.position = cellPosition + new Vector3(0, 1);
        _placed = true;
        PlaceAnimation(_placed);
        ActiveFigure = null;
        Deselect();
    }
    public abstract void Deactivate();
    #endregion

    #region Animation
    public void StartMyTurnAnimation(bool isMy)
    {
        _animator.SetBool("isMyTurn", isMy);
    }
    public void SelectAnimation(bool select)
    {
        _animator.SetBool("isSelect", select);
    }
    public void PlaceAnimation(bool isPlaced)
    {
        _animator.SetBool("isPlaced", isPlaced);
    }
    public void StartWinAnimation()
    {
        _animator.SetTrigger("isWin");
    }
    public void StartLoseAnimation()
    {
        _animator.SetTrigger("isLose");
    }
    public void StartLobbyAnimation()
    {
        _animator.SetBool("inLobby",true);
        if (PlayerId==1)
            _animator.SetTrigger("Attack");
    }
    public void GetHit()
    {
        if(!_invincinle)
            _animator.SetTrigger("GetHit");
    }
    public void Invincible()
    {
        _invincinle = true;
    }
    public void DeInvincible()
    {
        _invincinle = false;
    }
    #endregion


}
