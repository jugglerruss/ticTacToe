using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Intermidiate : Difficalty
{
    public Intermidiate(Game game, Board board, IDifficaltySwitcher difficaltySwitcher) : base(game, board, difficaltySwitcher)
    {
    }

    public override void DecisionWhatToDo()
    {
        var figures = GetNotPlacedFigure();
        if(CanWin(Player.My))
        {
            if (TryWinOrNotLose(Player.My, figures))
                return;
        }
        if (CanWin(PlayerSingle.Bot))
        {
            if(TryWinOrNotLose(PlayerSingle.Bot, figures))
                return;
        }
        if (PlacedFiguresCount() == 1)
        {
            DecisionFigure = GetRandomFigure();
            var lines = _board.GetAvalibleLines(GetPlacedFigure());
            var line = lines.ElementAt(Random.Range(0, lines.Count));
            var cellsAvalible = line.Where(c => c.GetCurrentFigure() == null || c.GetCurrentFigure().Strength <= DecisionFigure.Strength).ToList();
            if(cellsAvalible.Count != 0)
            {
                DecisionCell = cellsAvalible.FirstOrDefault();
            }
        }
        if (PlacedFiguresCount() == 2)
        {
            var placedFigures = PlacedFigures();
            var line = _board.GetLine(placedFigures[0], placedFigures[1]);
            if (IterateFigures(figures, line))
                return;
        }
        DecisionFigure = GetRandomFigure();
        DecisionCell = GetRandomCell(DecisionFigure);
    }
    private bool IterateFigures(List<Figure> figures, List<Cell> line)
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
        return false;
    }
    private bool CanWin(Player player)
    {
        if (player.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).Count() >= 2)
            return true;
        return false;
    }
    private bool TryWinOrNotLose(Player player,List<Figure> figures)
    {
        var linePlayerTwoFigures = _board.GetLinePlayerTwoFigures(player.Id);
        if (linePlayerTwoFigures != null)
        {
            if (IterateFigures(figures, linePlayerTwoFigures))
                return true;
        }
        return false;
    }

}
