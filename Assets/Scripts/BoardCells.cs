using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCells : MonoBehaviour
{

    [SerializeField] private Cell _cellDefault;
    [SerializeField] private int _cellsColumnCount;
    [SerializeField] private int _cellsRowCount;
    [SerializeField] private float _cellsWidth;
    [SerializeField] private float _startSpeed;
    private List<bool> _isInsert = new List<bool>();
    private Game _game;
    private void Start()
    {
        _game = FindObjectOfType<Game>();
        StartCoroutine(Move());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator Move()
    {
        _isInsert.Add(false);
        while (true)
        {
            _startSpeed = (float)_game.Score / 1000 + 1;
            MoveBoard(transform.position.x, _startSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    private void MoveBoard(float position, float step)
    {
        var i = Mathf.Abs((int)(position / _cellsWidth));
        if (!_isInsert[i])
        { 
            _isInsert[i] = true;
            _isInsert.Add(false);
            for (var j = 0; j < _cellsColumnCount; j++)
            {
                CreateCell(i, j);
            }
        }
        transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
    }

    private void CreateCell(int i, int j)
    {
        var cell = Instantiate(_cellDefault, transform, false);
        cell.SetRandomMaterial();
        cell.transform.position = new Vector3(
            cell.transform.position.x - i * _cellsWidth,
            cell.transform.position.y,
            cell.transform.position.z + j * _cellsWidth);
    }
}
