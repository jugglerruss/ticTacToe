using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : MonoBehaviour, IDifficaltySwitcher
{
    private Game _game => FindObjectOfType<Game>();
    private Board _board => FindObjectOfType<Board>();
    private int _difficalty => PlayerPrefs.GetInt("DifficultyValue", 0);
    private Difficalty _currentDifficalty;
    private List<Difficalty> _allDifficaltys;
    void Start()
    {
        _allDifficaltys = new List<Difficalty>()
        {
            new Beginner(_game,_board,this),
            new Intermidiate(_game,_board,this),
            new Hard(_game,_board,this)
        };
        _currentDifficalty = _allDifficaltys[_difficalty];
        StartCoroutine(WaitForTurn());
    }
    IEnumerator  WaitForTurn()
    {
        while (!_game.isOver)
        {
            yield return new WaitUntil(() => PlayerSingle.Bot.IsMyTurn);
            yield return new WaitForSeconds(1);
            _currentDifficalty.DecisionWhatToDo();
            var figure = _currentDifficalty.DecisionFigure;
            var cell = _currentDifficalty.DecisionCell;
            figure.Select();
            yield return new WaitForSeconds(1);
            cell.TryMoveFigure(PlayerSingle.Bot);
            PlayerSingle.Bot.ItsNotMyTurn(true);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void Switch<T>() where T : Difficalty //не используется, остиавил чтобы запомнить паттерн Состояние
    {
        var state = _allDifficaltys.FirstOrDefault(s => s is T);
        _currentDifficalty = state;
    }
}
