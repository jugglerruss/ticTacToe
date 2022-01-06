using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Cell[] _cellsLine1;
    [SerializeField]
    private Cell[] _cellsLine2;
    [SerializeField]
    private Cell[] _cellsLine3;

    private Cell[,] _cells = new Cell[4, 4];    
    public Cell[,] Cells { get; private set; }
    public List<List<Cell>> Lines { get; private set; }
    public List<List<Cell>> Crosses { get; private set; }
    private void Start()
    {
        for (var i = 1; i <= 3; i++) {
            _cells[1, i] = _cellsLine1[i-1];
            _cells[2, i] = _cellsLine2[i-1];
            _cells[3, i] = _cellsLine3[i-1];
        }
        Cells = _cells;
        Lines = new List<List<Cell>>()
        {
            new List<Cell>(){ _cells[1, 1],_cells[1, 2],_cells[1, 3]},
            new List<Cell>(){ _cells[2, 1],_cells[2, 2],_cells[2, 3]},
            new List<Cell>(){ _cells[3, 1],_cells[3, 2],_cells[3, 3]},
            new List<Cell>(){ _cells[1, 1],_cells[2, 1],_cells[3, 1]},
            new List<Cell>(){ _cells[1, 2],_cells[2, 2],_cells[3, 2]},
            new List<Cell>(){ _cells[1, 3],_cells[2, 3],_cells[3, 3]},
            new List<Cell>(){ _cells[1, 1],_cells[2, 2],_cells[3, 3]},
            new List<Cell>(){ _cells[1, 3],_cells[2, 2],_cells[3, 1]},

        };
        Crosses = new List<List<Cell>>()
        {
            new List<Cell>(){ _cells[2, 1],_cells[1, 2],_cells[1, 1]},
            new List<Cell>(){ _cells[2, 1],_cells[1, 2], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 2],_cells[2, 3], _cells[3, 1]},
            new List<Cell>(){ _cells[1, 2],_cells[2, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 2],_cells[2, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 2],_cells[2, 3], _cells[3, 3]},
            new List<Cell>(){ _cells[1, 2],_cells[2, 3], _cells[3, 1]},
            new List<Cell>(){ _cells[1, 2],_cells[2, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 1],_cells[1, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 1],_cells[3, 1], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 1],_cells[3, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 3],_cells[1, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 2],_cells[3, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 3],_cells[3, 2], _cells[2, 2]},
            new List<Cell>(){ _cells[2, 3],_cells[3, 1], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 3],_cells[2, 1], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 2],_cells[1, 1], _cells[2, 2]},
            new List<Cell>(){ _cells[3, 1],_cells[1, 2], _cells[2, 2]},
            new List<Cell>(){ _cells[2, 1],_cells[1, 3], _cells[2, 2]},
            new List<Cell>(){ _cells[1, 1],_cells[2, 3], _cells[2, 2]},
        };
    }
    public void ShowAvaliblePositions(Figure activeFigure)
    {
        for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                Cells[i, j].DoSelect(activeFigure);
    }
    public List<Cell> GetAvaliblePositions(Figure activeFigure)
    {
        List<Cell> avalibleCells = new List<Cell>();
        for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                if (Cells[i, j].IsAvaliblePosition(activeFigure))
                    avalibleCells.Add(Cells[i, j]);
        return avalibleCells;
    }
    public void HideAllPositions()
    {
        for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                Cells[i, j].DeSelect();
    }
    public int CheckLines()
    {
        foreach (var line in Lines)
        {
            if (line.Where(c => c.GetPlayerId() == 1).Count() == 3 ||
                line.Where(c => c.GetPlayerId() == 2).Count() == 3)
                return 1;
        }
        return 0;
    }
    public List<List<Cell>> GetAvalibleLines(Figure figure)
    {
        List<List<Cell>> avalibleLines = new List<List<Cell>>();
        foreach (var line in Lines)
            if (line.Where(c => c.GetCurrentFigure() == figure).Count() == 1)
                avalibleLines.Add(line);
        return avalibleLines;
    }
    public List<List<Cell>> GetAvalibleCrosses(Figure figure)
    {
        List<List<Cell>> avalibleCrosses = new List<List<Cell>>();
        foreach (var cross in Crosses)
            if (cross.Where(c => c.GetCurrentFigure() == figure).Count() == 1)
                avalibleCrosses.Add(cross);
        return avalibleCrosses;
    }
    public List<Cell> GetLine(Figure figure1, Figure figure2)
    {
        foreach (var line in Lines)
            if (line.Where(c => c.GetCurrentFigure() == figure1).Count() == 1 &&
                line.Where(c => c.GetCurrentFigure() == figure2).Count() == 1)
                return line;
        return null;
    }
    public List<Cell> GetLinePlayerTwoFigures(int playerId)
    {
        foreach (var line in Lines)
            if (line.Where(c => c.GetCurrentFigure() != null && c.GetCurrentFigure().PlayerId == playerId).Count() == 2)
                return line;
        return null;
    }
    public List<Cell> GetCrossPlayerThreeFigures(int playerId)
    {
        foreach (var cross in Crosses)
            if (cross.Where(c => c.GetCurrentFigure() != null && c.GetCurrentFigure().PlayerId == playerId).Count() == 3)
                return cross;
        return null;
    }
}
