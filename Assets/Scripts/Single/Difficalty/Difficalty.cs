using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Difficalty
{
    protected readonly Game _game;
    protected readonly Board _board;
    protected readonly IDifficaltySwitcher _difficaltySwitcher;
    public Figure DecisionFigure { get; protected set; }
    public Cell DecisionCell{ get; protected set; }

protected Difficalty(Game game, Board board, IDifficaltySwitcher difficaltySwitcher)
    {
        _game = game;
        _board = board;
        _difficaltySwitcher = difficaltySwitcher;
    }
    public abstract void DecisionWhatToDo();
    protected List<Figure> GetNotPlacedFigure()
    {
        return PlayerSingle.Bot.Figures.Where(f => !f.isPlaced).ToList();
    }
    protected Figure GetRandomFigure()
    {
        var figures = GetNotPlacedFigure();
        return GetNotPlacedFigure().ElementAt(Random.Range(0, figures.Count));
    }
    protected Figure GetFirstFigure()
    {
        var figures = GetNotPlacedFigure();
        return figures.ElementAt(0);
    }
    protected Cell GetRandomCell(Figure figure)
    {
        var cells = _board.GetAvaliblePositions(figure);
        return cells.ElementAt(Random.Range(0, cells.Count));
    }
    protected int PlacedFiguresCount()
    {
        return PlayerSingle.Bot.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).Count();
    }
    protected Figure[] PlacedFigures()
    {
        return PlayerSingle.Bot.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).ToArray();
    }
    protected Figure GetPlacedFigure()
    {
        return PlayerSingle.Bot.Figures.Where(f => f.isPlaced && f.gameObject.activeSelf).FirstOrDefault();
    }
}
