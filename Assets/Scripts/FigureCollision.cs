using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FigureCollision f))
        {
            Debug.LogError("collisionFigure");
            transform.parent.GetComponent<Figure>().GetHit();
        }
    }
}
