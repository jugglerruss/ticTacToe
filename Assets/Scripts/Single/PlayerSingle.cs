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
            var start = 0f;
            if (i==1)
                start = 0.1f;
            figures[i] = Instantiate(_figurePrefab, transform.localPosition + new Vector3(i + start, 0, 0), _figurePrefab.transform.rotation,transform).gameObject;
            figures[i].GetComponent<Figure>().SetStrength(COUNT_FIGURES - i);
        }
        return figures;
    }
    public void ItsMyTurn(bool deactivateOthers)
    {
        if (!game.isOver)
            if (Figures.Where(f => !f.isPlaced).Count() > 0)
                MyTurn(true);
            else
                NoFiguresDraw?.Invoke();
        if (deactivateOthers)
            if (this == Bot)
                My.MyTurn(false);
            else
                Bot.MyTurn(false);
    }
    public void ItsNotMyTurn(bool activateOthers)
    {
        MyTurn(false);
        if (activateOthers)
            if (this == Bot)
                My.MyTurn(true);
            else
                Bot.MyTurn(true);
    }
}
