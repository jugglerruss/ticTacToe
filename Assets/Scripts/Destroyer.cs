using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cell>(out Cell cell))
           Destroy(cell.gameObject);
    }
}
