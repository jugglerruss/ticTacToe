using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Game : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnWin;
    [SerializeField] protected UnityEvent OnLose;
    [SerializeField] protected UnityEvent OnDraw;
    [SerializeField] protected UI _ui;
    public bool isOver { get; protected set; }
    protected Board _board => FindObjectOfType<Board>();
    protected List<Player> _players = new List<Player>();
    public virtual string GameType { get; set; }

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

    public void SetScoreWin()
    {
        PlayerPrefs.SetInt("score" + GameType + "Win", PlayerPrefs.GetInt("score" + GameType + "Win", 0) + 1);
        _ui.SetScoreInfo(GameType);
    }

    public void SetScoreLose()
    {
        PlayerPrefs.SetInt("score" + GameType + "Lose", PlayerPrefs.GetInt("score" + GameType + "Lose", 0) + 1);
        _ui.SetScoreInfo(GameType);
    }

    public void SetScoreDraw()
    {
        PlayerPrefs.SetInt("score" + GameType + "Draw", PlayerPrefs.GetInt("score" + GameType + "Draw", 0) + 1);
        _ui.SetScoreInfo(GameType);
    }
    protected virtual void AnimateBot(bool win){    }

}
