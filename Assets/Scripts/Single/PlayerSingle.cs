using UnityEngine;

public class PlayerSingle : Player
{
    protected override GameObject[] InstantiateFigures()
    {
        GameObject[] figures = new GameObject[6];
        for (var i = 0; i < 6; i++)
        {
            figures[i] = Instantiate(_figurePrefab, transform.position, _figurePrefab.transform.rotation).gameObject;
        }
        return figures;
    }

    protected override void Start()
    {
        GameObject[] figuresGameObjects;
        figuresGameObjects = InstantiateFigures();
        Figures = new Figure[figuresGameObjects.Length];
        for (var i = 0; i < figuresGameObjects.Length; i++)
            Figures[i] = figuresGameObjects[i].GetComponent<Figure>();
    }
}
