using Photon.Pun;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class Cell : MonoBehaviour
{
    [SerializeField]
    protected Material _selectMaterial;

    protected Figure _currentFigure;
    protected MeshRenderer _meshRender => GetComponent<MeshRenderer>();
    protected Material _defaultMaterial => _meshRender.material;
    protected void OnMouseDown()
    {
        if( Player.My.IsMyTurn && Figure.ActiveFigure != null)
            if (Player.My.Figures.Contains(Figure.ActiveFigure))
            {
                if (_currentFigure != null)
                    if (!CompareWithCurrentFigure(Figure.ActiveFigure))
                        return;
                MoveFigureOnPosition();
            }        
    }

    public void DoSelect(Figure activeFigure)
    {
        if (_currentFigure==null || activeFigure.Strength > _currentFigure.Strength)
            _meshRender.sharedMaterial.color = new Color(1, 1, 0);
    }
    public void DeSelect()
    {
        _meshRender.sharedMaterial.color = new Color(1, 1, 1);
    }
    public int GetPlayerId()
    {
        if(_currentFigure!=null) return _currentFigure.PlayerId;
        return 0;
    }
    protected bool CompareWithCurrentFigure(Figure activeFigure)
    {
        if (activeFigure.Strength > _currentFigure.Strength)
        {
            _currentFigure.Deactivate();
            return true;
        }
        Debug.LogError("Текущая фигура меньше размещённой");
        return false;
    }
    protected abstract void MoveFigureOnPosition();
}
