using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Hard : Difficalty
{
    public Hard(Game game, Board board, IDifficaltySwitcher difficaltySwitcher) : base(game, board, difficaltySwitcher)
    {
    }
    public override void DecisionWhatToDo()
    {
        var figures = GetNotPlacedFigure();
        if (TryWin(PlayerSingle.Bot, figures))
            return;
        if (TryNotLose(Player.My, figures))
            return;

        if (PlacedFiguresCount() == 1)
        {
            if (ChoseCross())
                return;
        }
        DecisionFigure = GetRandomFigure();
        DecisionCell = GetRandomCell(DecisionFigure);
    }
    private bool ChoseCross()
    {
        DecisionFigure = GetRandomFigure();
        var crosses = _board.GetAvalibleCrosses(GetPlacedFigure());
        var cross = crosses.ElementAt(Random.Range(0, crosses.Count));
        var cellsAvalible = cross
            .Where(c => c.GetCurrentFigure() == null ||
                  (c.GetCurrentFigure().Strength <= DecisionFigure.Strength &&
                   c.GetCurrentFigure().PlayerId != DecisionFigure.PlayerId))
            .ToList();
        if (cellsAvalible.Count != 0)
        {
            DecisionCell = cellsAvalible.FirstOrDefault();
            return true;
        }
        return false;
    }
    private bool IterateFigures(List<Figure> figures, List<Cell> line)
    {
        if (line != null)
        {
            var i = 0;
            while (figures.Count > i)
            {
                DecisionFigure = figures.ElementAt(i);
                var cell = line.Where(c => c.GetCurrentFigure() == null ||
                (c.GetCurrentFigure().Strength <= DecisionFigure.Strength && c.GetCurrentFigure().PlayerId != DecisionFigure.PlayerId)).FirstOrDefault();
                if (cell != null)
                {
                    DecisionCell = cell;
                    return true;
                }
                i++;
            }
        }
        return false;
    }
    private bool DestroyCross(List<Figure> figures, List<Cell> cross)
    {
        if (cross != null)
        {
            if (ChoseFigureForCross(cross, figures, true))
                return true;
            if (ChoseFigureForCross(cross, figures, false))
                return true;
        }
        return false;
    }
    private bool ChoseFigureForCross(List<Cell> cross, List<Figure> figures, bool haveStrongerFigure)
    {
        var i = 0;
        while (figures.Count > i)
        {
            DecisionFigure = figures.ElementAt(i);
            var cell = cross.ElementAt(2);
            if (haveStrongerFigure)
            {
                if (cell.GetCurrentFigure().Strength < DecisionFigure.Strength && cell.GetCurrentFigure().PlayerId != DecisionFigure.PlayerId)
                {
                    DecisionCell = cell;
                    return true;
                }
            }
            else
            {
                if (cell.GetCurrentFigure().Strength == DecisionFigure.Strength && cell.GetCurrentFigure().PlayerId != DecisionFigure.PlayerId)
                {
                    DecisionCell = cell;
                    return true;
                }
            }
            i++;
        }
        return false;
    }
    private bool TryWin(Player player, List<Figure> figures)
    {
        if (player.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).Count() >= 2)
        {
            var linePlayerTwoFigures = _board.GetLinePlayerTwoFigures(player.Id);
            if (linePlayerTwoFigures != null)
            {
                Debug.LogError("TryWin");
                if (IterateFigures(figures, linePlayerTwoFigures))
                    return true;
            }
        }
        return false;

    }
    private bool TryNotLose(Player player, List<Figure> figures)
    {
        var figuresCount = player.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).Count();
        if (figuresCount >= 3)
        {
            var cross = _board.GetCrossPlayerThreeFigures(player.Id);
            if (cross != null)
            {
                if (DestroyCross(figures, cross))
                    return true;
            }
        }
        if (figuresCount >= 2)
        {
            var linePlayerTwoFigures = _board.GetLinePlayerTwoFigures(player.Id);
            if (linePlayerTwoFigures != null)
            {
                if (IterateFigures(figures, linePlayerTwoFigures))
                    return true;
            }
        }
        return false;

    }
}
