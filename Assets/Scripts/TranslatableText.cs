using TMPro;
using UnityEngine;

public class TranslatableText : MonoBehaviour
{
    [SerializeField] private int _textID;
    public int TextID => _textID;
    private TextMeshPro _text;
    private TextMeshProUGUI _textUI;
    public void SetText(string text)
    {
        if(_text !=null)_text.text = text;
        if (_textUI != null) _textUI.text = text;
    }
    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _textUI = GetComponent<TextMeshProUGUI>();
        Translator.Instance.Add(this);
    }
    private void OnEnable()
    {
        Translator.Instance.UpdateTexts();
    }
    private void OnDestroy()
    {
        Translator.Instance.Remove(this);
    }
}
