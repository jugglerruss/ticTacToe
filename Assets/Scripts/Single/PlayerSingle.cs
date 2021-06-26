using System;
using System.Linq;
using UnityEngine;

public class PlayerSingle : Player
{
    public event Action NoFiguresDraw;
    public static PlayerSingle Bot;
    public static new PlayerSingle My {
        get 
        {
            return Player.My as PlayerSingle;
        }
        set
        {
            Player.My = value;
        }
    }
    protected override void Awake()
    {
        GameObject[] figuresGameObjects;
        figuresGameObjects = InstantiateFigures();
        Figures = new Figure[figuresGameObjects.Length];
        for (var i = 0; i < figuresGameObjects.Length; i++)
            Figures[i] = figuresGameObjects[i].GetComponent<Figure>();
    }
    protected override GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[COUNT_FIGURES];
        for (var i = 0; i < COUNT_FIGURES; i++)
        {
            figures[i] = Instantiate(_figurePrefab, transform.position, _figurePrefab.transform.rotation,transform).gameObject;
            figures[i].transform.localScale -= new Vector3(SCALE_FIGURE, SCALE_FIGURE, SCALE_FIGURE) * i;
            figures[i].transform.localPosition = _figurePrefab.transform.localPosition + new Vector3(i * 1.4f - i * 4 * SCALE_FIGURE, 0, 0);
            figures[i].GetComponent<Figure>().Strength = COUNT_FIGURES - i;
        }
        return figures;
    }
    public void ItsMyTurn(bool deactivateOthers)
    {
        if (Figures.Where(f => !f.isPlaced).Count() > 0)
            IsMyTurn = true;
        else
            NoFiguresDraw?.Invoke();
        if (deactivateOthers)
            if (this == Bot)
                My.IsMyTurn = false;
            else
                Bot.IsMyTurn = false;
    }
    public void ItsNotMyTurn(bool activateOthers)
    {
        IsMyTurn = false;
        if (activateOthers)
            if (this == Bot)
                My.IsMyTurn = true;
            else
                Bot.IsMyTurn = true;
    }
}
