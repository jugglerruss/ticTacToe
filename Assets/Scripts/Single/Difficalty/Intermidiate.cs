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
        if (TryWinOrNotLose(PlayerSingle.Bot, figures))
            return;
        if (TryWinOrNotLose(Player.My, figures))
            return;        
        
        if (PlacedFiguresCount() == 1)
        {
            if (ChoseLine())
                return;
        }
        DecisionFigure = GetRandomFigure();
        DecisionCell = GetRandomCell(DecisionFigure);
    }
    private bool ChoseLine()
    {
        DecisionFigure = GetRandomFigure();
        var lines = _board.GetAvalibleLines(GetPlacedFigure());
        var line = lines.ElementAt(Random.Range(0, lines.Count));
        var cellsAvalible = line.Where(c => c.GetCurrentFigure() == null || c.GetCurrentFigure().Strength <= DecisionFigure.Strength).ToList();
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
    private bool TryWinOrNotLose(Player player,List<Figure> figures)
    {
        if (player.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).Count() >= 2)
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
