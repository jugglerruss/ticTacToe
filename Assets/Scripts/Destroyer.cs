using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    private Game _game;
    private int _clickCount;
    private void Start()
    {
        _game = FindObjectOfType<Game>();
    }
    private void OnMouseDown()
    {
        StopCoroutine(Zeroing());
        AudioManager.Instance.PlayPortal();
        _game.SetColor();
        _clickCount++;
        CheckAchievements(_clickCount);
        StartCoroutine(Zeroing());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cell>(out Cell cell))
           Destroy(cell.gameObject);
    }
    private IEnumerator Zeroing()
    {
        yield return new WaitForSeconds(1);
        _clickCount = 0;
    }
    private void CheckAchievements(int clickCount)
    {
        string id;
        switch (clickCount)
        {
            case 1:
                id = GPS.achievement_toch_the_hole;
                break;
            case 3:
                id = GPS.achievement_black_hole_or_not_so_black;
                break;;
            default: return;
        }
        PlayServices.Instance.UnlockAchievement(id);
    }
}
