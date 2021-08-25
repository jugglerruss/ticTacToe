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
        _game.SetRandomColor();
        _clickCount++;
        CheckAchievements.Instance.CheckAchievementsHole(_clickCount);
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
   
}
