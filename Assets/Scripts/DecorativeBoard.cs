using System.Collections;
using UnityEngine;

public class DecorativeBoard : MonoBehaviour
{
    [SerializeField] private GameObject _cellDefault;
    [SerializeField] private int _cellsColumnCount;
    [SerializeField] private int _cellsRowCount;
    private Cell[,] _cells { get; set; }

    private void Start()
    {
        _cells = new Cell[_cellsRowCount, _cellsColumnCount];
        CreateCellsAndSetRows();

        StartCoroutine(Move());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void CreateCellsAndSetRows()
    {
        for (var i = 0; i < _cellsRowCount; i++)
        {
            for (var j = 0; j < _cellsColumnCount; j++)
            {
                var cell = Instantiate(_cellDefault, transform, false);
                var cellComp = cell.GetComponent<Cell>();
                cellComp.SetRandomMaterial();
                _cells[i, j] = cellComp;
                cell.transform.position = new Vector3(
                    cell.transform.position.x + i * 2.1f,
                    cell.transform.position.y,
                    cell.transform.position.z + j * 2.1f);
            }
        }
    }
    private IEnumerator Move()
    {
        var xPos = transform.position.x;
        var xPosEnd = -15;
        var step = 0.005f;
        var showHideMeter = 2.2f;
        while (true)
        {
            while (transform.position.x > xPosEnd)
            {
                MoveBoard(transform.position.x, showHideMeter, step, -1);
                yield return new WaitForEndOfFrame();
            }
            while (transform.position.x < xPos)
            {
                MoveBoard(transform.position.x, showHideMeter, step, 1);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    private void MoveBoard(float position, float meter,float step, int direction)
    {
        var surplus = position % meter;
        var right = direction < 0;
        var abs = Mathf.Abs((int)(position / meter));
        if (surplus <= step && abs > 0)
        {
            for (var j = 0; j < _cellsColumnCount; j++)
            {
                _cells[abs + 4, j].ShoeHideLobby(right);
                _cells[abs - 1, j].ShoeHideLobby(!right);
            }
        }
        transform.position = new Vector3(transform.position.x + direction * step, transform.position.y, transform.position.z);
    }
}
