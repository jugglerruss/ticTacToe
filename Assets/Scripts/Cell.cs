using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Cell : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleClick;
    [SerializeField] private MeshRenderer _sphare;
    [SerializeField] private MeshRenderer _body;
    [SerializeField] private UnityEvent _clicked;
    [SerializeField] private List<Material> _materials;

    private bool _isClicked = false;
    private Game _game => FindObjectOfType<Game>();
    public Color Color => _body.material.color;
    public bool IsClicked => _isClicked;
    public event UnityAction Clicked
    {
        add => _clicked.AddListener(value);
        remove => _clicked.RemoveListener(value);
    }
    private void OnMouseDown()
    {
        if (_isClicked)
            return;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LobbyClick();
        else
            PressCell();
    }
    public void PressCell()
    {
        _sphare.gameObject.SetActive(false);
        _isClicked = true;
        if (_game.CompareColors(_body.material.color))
            ShowEffect();
    }
    public void ShoeHideLobby(bool show)
    {
        if (show)
        {
            _isClicked = false;
            _sphare.gameObject.SetActive(show);
        }        
    }
    private void LobbyClick()
    {
        ShowEffect();
        SetRandomMaterial();
        _sphare.gameObject.SetActive(false);
        _isClicked = true;
        AudioManager.Instance.PlayPop(0);
    }

    private void ShowEffect()
    {
        var particleMain = Instantiate(_particleClick, transform).main;
        particleMain.startColor = _body.material.color;
    }

    public void SetRandomMaterial()
    {
        var rand = Random.Range(0, _materials.Count);
        var material = _materials.ElementAt(rand);
        _body.material = material;
        _sphare.material = material;
    }
}
