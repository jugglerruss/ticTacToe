using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private UnityEvent _clicked;
    private void OnMouseDown()
    {
        AudioManager.Instance.PlayPortal();
        _clicked.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cell>(out Cell cell))
           Destroy(cell.gameObject);
    }
}
