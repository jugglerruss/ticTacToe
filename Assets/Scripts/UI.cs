using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text WinLoseText;
    [SerializeField] private Button ButtonReset;
    [SerializeField] private TextMeshProUGUI _myNickName;
    [SerializeField] private TextMeshProUGUI _enemyNickName;
    public void MessageWinLose(bool win)
    {
        ButtonReset.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);
        if (win)
            WinLoseText.text = "Победа";
        else
            WinLoseText.text = "Поражение";
    }
    public void MessageDraw()
    {
        ButtonReset.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);
        WinLoseText.text = "Ничья";
    }
    public void SetMyNickName(string name)
    {
        _myNickName.text = name;
    }
    public void SetEnemyNickName(string name)
    {
        _enemyNickName.text = name;
    }
    public void SetBotName(string name)
    {
        _enemyNickName.text = string.Format(_enemyNickName.text, name);
    }

}
