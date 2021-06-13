using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text WinLoseText;

    public void MessageWinLose(bool win)
    {
        WinLoseText.gameObject.SetActive(true);
        if (win)
            WinLoseText.text = "Победа";
        else
            WinLoseText.text = "Поражение";
    }
}
