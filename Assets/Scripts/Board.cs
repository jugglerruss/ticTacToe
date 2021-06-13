using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [SerializeField]
    private ClickPosition[] _cellsLine1;
    [SerializeField]
    private ClickPosition[] _cellsLine2;
    [SerializeField]
    private ClickPosition[] _cellsLine3;
    [SerializeField]
    private UnityEvent OnWin;
    [SerializeField]
    private UnityEvent OnLose;

    private ClickPosition[,] cells = new ClickPosition[4, 4];
    
    public static ClickPosition[,] Cells;
    public static Board MyBoard;
    private void Start()
    {
        MyBoard = this;
        if (OnWin == null)
            OnWin = new UnityEvent();
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
    public bool CheckWin(Figure currentFigure)
    {
        if (CheckLines() != 0)
        {
            if (currentFigure.photonView.IsMine)
            {
                OnWin.AddListener(Player.MyPlayer.Victory);
                OnWin.Invoke();
            }
            else
            {
                OnLose.AddListener(Player.MyPlayer.Lose);
                OnLose.Invoke();
            }                
        }
        return false;
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
