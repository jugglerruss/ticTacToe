using UnityEngine;

public class CellSingle : Cell
{
    protected override void MoveFigureOnPosition()
    {
        var activeFigure = Figure.ActiveFigure;
        var isPlayerOwner = activeFigure.CheckOwner(PlayerSingle.My);
        if (Figure.ActiveFigure.PlaceInPosition(transform.position))
        {
            _currentFigure = activeFigure;
            if (!_game.CheckWin(_currentFigure))
            {
                if (isPlayerOwner)
                    PlayerSingle.Bot.ItsMyTurn(true);
                else
                    PlayerSingle.My.ItsMyTurn(true);
            }
        }
        else
        {
            if (isPlayerOwner)
                PlayerSingle.Bot.ItsMyTurn(true);
            else
                PlayerSingle.My.ItsMyTurn(true);
        }
    }
}
