using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{

    [SerializeField] private Button _buttonMute;
    [SerializeField] private Button _buttonUnMute;

    public void ShowHideMuteButtons(bool mute)
    {
        _buttonMute.gameObject.SetActive(!mute);
        _buttonUnMute.gameObject.SetActive(mute);
    }
}
