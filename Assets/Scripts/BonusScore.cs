using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusScore : MonoBehaviour
{
    [SerializeField] private float _speedY;
    [SerializeField] private float _speedAlpha;
    private TextMeshPro _tmp; 
    void Start()
    {
        _tmp = GetComponent<TextMeshPro>();
        _tmp.alpha = 0;
    }
    private IEnumerator MoveHide()
    {
        _tmp.alpha = 1;
        var y = 0f;
        while (_tmp.alpha > 0)
        {
            y += _speedY * Time.deltaTime;
            transform.position += new Vector3(0, y, 0);
            _tmp.alpha -= _speedAlpha * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
    }
    public void Activate(int score, Cell cell)
    {
        _tmp.text = score.ToString();
        transform.position = cell.transform.position;
        StartCoroutine(MoveHide());
    }
}
