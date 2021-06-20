using Photon.Pun;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class Cell : MonoBehaviour
{
    [SerializeField]
    protected Material _selectMaterial;
    [SerializeField]
    protected Material _defaultMaterial;

    protected Figure _currentFigure;
    protected MeshRenderer _meshRender => GetComponent<MeshRenderer>();
    protected Game _game => FindObjectOfType<Game>();
    protected void OnMouseDown()
    {
        TryMoveFigure(Player.My);
    }
    public void TryMoveFigure(Player player)
    {
        if (player.IsMyTurn && Figure.ActiveFigure != null)
            if (player.Figures.Contains(Figure.ActiveFigure))
            {
                if (!CompareWithCurrentFigure(Figure.ActiveFigure))
                        return;
                MoveFigureOnPosition();
            }
    }
    public void DoSelect(Figure activeFigure)
    {
        if (IsAvaliblePosition(activeFigure))
            _meshRender.sharedMaterial = _selectMaterial;
    }
    public bool IsAvaliblePosition(Figure activeFigure)
    {
        return _currentFigure == null || (activeFigure.Strength > _currentFigure.Strength && activeFigure.PlayerId != _currentFigure.PlayerId);           
    }
    public void DeSelect()
    {
        _meshRender.sharedMaterial = _defaultMaterial;
    }
    public int GetPlayerId()
    {
        if(_currentFigure!=null) return _currentFigure.PlayerId;
        return 0;
    }
    protected bool CompareWithCurrentFigure(Figure activeFigure)
    {
        if (IsAvaliblePosition(activeFigure))
        {
            _currentFigure?.Deactivate();
            return true;
        }
        Debug.LogError("Текущая фигура меньше размещённой");
        return false;
    }
    protected abstract void MoveFigureOnPosition();
}
