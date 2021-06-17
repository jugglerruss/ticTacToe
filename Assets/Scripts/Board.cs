using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Cell[] _cellsLine1;
    [SerializeField]
    private Cell[] _cellsLine2;
    [SerializeField]
    private Cell[] _cellsLine3;

    private Cell[,] cells = new Cell[4, 4];
    
    public static Cell[,] Cells;
    public static Board My;
    private void Start()
    {
        My = this;
        for (var i = 1; i <= 3; i++) {
            cells[1, i] = _cellsLine1[i-1];
            cells[2, i] = _cellsLine2[i-1];
            cells[3, i] = _cellsLine3[i-1];
        }
        Cells = cells;
    }
    public void ShowAvaliblePositions(Figure activeFigure)
    {
        for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                Cells[i, j].DoSelect(activeFigure);
    }
    public void HideAllPositions()
    {
        for (var i = 1; i <= 3; i++)
            for (var j = 1; j <= 3; j++)
                Cells[i, j].DeSelect();
    }
    public int CheckLines()
    {        
        for (var j = 1; j <= 3; j++)
            if ( Cells[1, j].GetPlayerId() == Cells[2, j].GetPlayerId() && Cells[3, j].GetPlayerId() == Cells[2, j].GetPlayerId())
                if(Cells[1, j].GetPlayerId()!=0)
                    return Cells[1, j].GetPlayerId();
        for (var i = 1; i <= 3; i++)
            if ( Cells[i,1].GetPlayerId() == Cells[i,2].GetPlayerId() && Cells[i,3].GetPlayerId() == Cells[i,2].GetPlayerId())
                if (Cells[i, 1].GetPlayerId() != 0)
                    return Cells[i, 1].GetPlayerId();
        if (Cells[1, 1].GetPlayerId() == Cells[2, 2].GetPlayerId() && Cells[2, 2].GetPlayerId() == Cells[3, 3].GetPlayerId())
            if (Cells[1, 1].GetPlayerId() != 0)
                return Cells[1, 1].GetPlayerId();
        if (Cells[3, 1].GetPlayerId() == Cells[2, 2].GetPlayerId() && Cells[2, 2].GetPlayerId() == Cells[1, 3].GetPlayerId())
            if (Cells[3, 1].GetPlayerId() != 0)
                return Cells[3, 1].GetPlayerId();

        return 0;
    }
}
