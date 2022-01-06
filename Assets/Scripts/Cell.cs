using Photon.Pun;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class Cell : MonoBehaviour
{
    [SerializeField] protected Material _selectMaterial;
    [SerializeField] protected Material _defaultMaterial;

    protected Figure _currentFigure;
    protected MeshRenderer _meshRender => GetComponent<MeshRenderer>();
    protected Game _game => FindObjectOfType<Game>();
    protected void OnMouseDown()
    {
        TryMoveFigure(Player.My);
    }
    public bool TryMoveFigure(Player player)
    {
        if (player.IsMyTurn && Figure.ActiveFigure != null && IsAvaliblePosition(Figure.ActiveFigure))
        {
            if (_currentFigure == null)
                MoveFigureOnPosition();
            else
                CompareWithCurrentFigure();
                 
            return true;
        }
        return false;
    }
    public void DoSelect(Figure activeFigure)
    {
        if (IsAvaliblePosition(activeFigure))
            _meshRender.material = _selectMaterial;
    }
    public bool IsAvaliblePosition(Figure activeFigure)
    {
        return _currentFigure == null || activeFigure.Strength >= _currentFigure.Strength || activeFigure.PlayerId == _currentFigure.PlayerId;           
    }
    public void DeSelect()
    {
        _meshRender.material = _defaultMaterial;
    }
    public int GetPlayerId()
    {
        if(_currentFigure!=null) return _currentFigure.PlayerId;
        return 0;
    }
    public Figure GetCurrentFigure()
    {
        return _currentFigure;
    }
    protected void CompareWithCurrentFigure()
    {
        if (Figure.ActiveFigure.PlayerId == _currentFigure.PlayerId)
        {
            Figure.ActiveFigure.SetStrength(Figure.ActiveFigure.Strength + _currentFigure.Strength);
        }
        else
        {
            Figure.ActiveFigure.SetStrength(Figure.ActiveFigure.Strength - _currentFigure.Strength);
        }
        _currentFigure?.Deactivate();
        _currentFigure = null;
        MoveFigureOnPosition();
    }
    protected abstract void MoveFigureOnPosition();
}
