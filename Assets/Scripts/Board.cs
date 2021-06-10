using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private ClickPosition[] _cellsLine1;
    [SerializeField]
    private ClickPosition[] _cellsLine2;
    [SerializeField]
    private ClickPosition[] _cellsLine3;

    private ClickPosition[,] cells = new ClickPosition[4, 4];
    
    public static ClickPosition[,] Cells;
    private void Start()
    {
        for (var i = 1; i <= 3; i++) {
            cells[1, i] = _cellsLine1[i-1];
            cells[2, i] = _cellsLine2[i-1];
            cells[3, i] = _cellsLine3[i-1];
        }
        Cells = cells;
    }
    public static bool CheckWin(DragDrop currentFigure)
    {
        if (CheckLines() != 0 && currentFigure.photonView.IsMine) Debug.LogError(PhotonNetwork.NickName + " win");
        return false;
    }
    public static int CheckLines()
    {
        
        for (var j = 1; j <= 3; j++)
            if ( Cells[1, j].GetPlayerId() == Cells[2, j].GetPlayerId() && Cells[3, j].GetPlayerId() == Cells[2, j].GetPlayerId())
                return Cells[1, j].GetPlayerId();
        for (var i = 1; i <= 3; i++)
            if ( Cells[i,1].GetPlayerId() == Cells[i,2].GetPlayerId() && Cells[i,3].GetPlayerId() == Cells[i,2].GetPlayerId())
                return Cells[i, 1].GetPlayerId();
        if (Cells[1, 1].GetPlayerId() == Cells[2, 2].GetPlayerId() && Cells[2, 2].GetPlayerId() == Cells[3, 3].GetPlayerId())
            return Cells[1, 1].GetPlayerId();
        if (Cells[3, 1].GetPlayerId() == Cells[2, 2].GetPlayerId() && Cells[2, 2].GetPlayerId() == Cells[1, 3].GetPlayerId())
            return Cells[3, 1].GetPlayerId();

        return 0;
    }
}
