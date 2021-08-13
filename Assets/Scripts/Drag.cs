using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float _position;
    private void OnMouseDrag()
    {
        if (Input.mousePosition.y > _position)
        {
            Time.timeScale = 3f;
            AudioManager.Instance.PlayDrag();
        }
        else
        {
            StopCoroutine(SlowDown());
            StartCoroutine(SlowDown());
        }
            
        _position = Input.mousePosition.y;
    }
    private void OnMouseUp()
    {
        StopCoroutine(SlowDown());
        StartCoroutine(SlowDown());
    }
    private IEnumerator SlowDown()
    {
        while (Time.timeScale > 1)
        {
            Time.timeScale -= 0.01f;
            yield return new WaitForEndOfFrame();
        }
        AudioManager.Instance.StopDrag();
    }
}
