using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FigureCollision f))
        {
            transform.parent.GetComponent<Figure>().GetHit();
        }
    }
}
