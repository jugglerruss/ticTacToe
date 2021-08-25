using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsWait : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitInteract());
    }
    private void OnDisable()
    {
        GetComponent<Button>().interactable = false;
        StopCoroutine(WaitInteract());
    }
    private void OnDestroy()
    {
        StopCoroutine(WaitInteract());
    }
    private IEnumerator WaitInteract()
    {
        while (Input.touchCount > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Button>().interactable = true;
    }
}
