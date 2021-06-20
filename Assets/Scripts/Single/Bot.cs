using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private Game _game => FindObjectOfType<Game>();
    private Board _board => FindObjectOfType<Board>();
    void Start()
    {
        var wait = StartCoroutine(WaitForTurn());
    }
    IEnumerator  WaitForTurn()
    {
        while (!_game.isOver)
        {
            yield return new WaitUntil(() => PlayerSingle.II.IsMyTurn);
            yield return new WaitForSeconds(1);
            var figure = GetRandomFigure();
            var position = GetRandomPosition(figure);
            figure.BeginDrag();
            yield return new WaitForSeconds(1);
            position.TryMoveFigure(PlayerSingle.II);
            PlayerSingle.II.ItsNotMyTurn(true);
            yield return new WaitUntil(() => PlayerSingle.II.IsMyTurn);
        }
    }
    private Figure GetRandomFigure()
    {
        var figures = PlayerSingle.II.Figures.Where(f => !f.isPlaced).ToList();
        return figures.ElementAt(Random.Range(0, figures.Count));
    }
    private Cell GetRandomPosition(Figure figure)
    {
        var positions = _board.GetAvaliblePositions(figure);
        return positions.ElementAt(Random.Range(0, positions.Count));
    }
}
