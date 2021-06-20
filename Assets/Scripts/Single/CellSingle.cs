using UnityEngine;

public class CellSingle : Cell
{
    protected override void MoveFigureOnPosition()
    {
        _currentFigure = Figure.ActiveFigure;
        _currentFigure.PlaceInPosition(transform.position);
        if (!_game.CheckWin(_currentFigure))
        {
            if (_currentFigure.CheckOwner(PlayerSingle.My))
                PlayerSingle.II.ItsMyTurn(true);
            else
                PlayerSingle.My.ItsMyTurn(true);
        }
    }
}
