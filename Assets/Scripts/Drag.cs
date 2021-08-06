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
            AudioManager.Instance.StopDrag();
            Time.timeScale = 1f;
        }
            
        _position = Input.mousePosition.y;
    }
    private void OnMouseUp()
    {
        AudioManager.Instance.StopDrag();
        Time.timeScale = 1f;
    }
}
