using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float _position;
    private int _dragCount;
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
        CheckAchievements(_dragCount);
        while (Time.timeScale > 1)
        {
            Time.timeScale -= 0.01f;
            yield return new WaitForEndOfFrame();
        }
        _dragCount++;
        AudioManager.Instance.StopDrag();
    }
    private void CheckAchievements(int dragCount)
    {
        string id;
        switch (dragCount)
        {
            case 1:
                id = GPS.achievement_i_can_rule_the_time;
                break;
            case 10:
                id = GPS.achievement_speed_up;
                break; ;
            case 50:
                id = GPS.achievement_need_for_speed;
                break; ;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
}
