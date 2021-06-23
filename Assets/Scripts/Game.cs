using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent OnWin;
    [SerializeField]
    protected UnityEvent OnLose;
    [SerializeField]
    protected UnityEvent OnDraw;
    public bool isOver { get; protected set; }
    private Board _board => FindObjectOfType<Board>();
    protected Player[] _players = new Player[2];

    public bool CheckWin(Figure currentFigure)
    {
        if (_board.CheckLines() != 0)
        {
            isOver = true;
            if (currentFigure.CheckOwner(Player.My))
            {
                Player.My.Victory();
                OnWin.Invoke();
                AnimateBot(false);
            }
            else
            {
                Player.My.Lose();
                OnLose.Invoke();
                AnimateBot(true);
            }
        }
        else
        {

        }
        return false;
    }

    protected void Player_NoFiguresDraw()
    {
        OnDraw.Invoke();
    }
    protected virtual void AnimateBot(bool win){    }

}
