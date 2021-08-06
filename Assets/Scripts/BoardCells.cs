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
    [SerializeField] private float _step;
    private List<bool> _isInsert = new List<bool>();
    private void Start()
    {

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
            MoveBoard(transform.position.x, _step * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            Debug.Log(Time.deltaTime);
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
