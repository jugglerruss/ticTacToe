using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _teleportlIn;
    [SerializeField] private ParticleSystem _teleportlOut;
    [SerializeField] private Light _teleportlLight;
    [SerializeField] private List<Material> _cellsMaterials;

    public void SetMaterialTint(Color color)
    {
        var main = _teleportlIn.main;
        _teleportlIn.Stop();
        color.a = 0.5f;
        main.startColor = color;
        _teleportlIn.Play();
        _teleportlOut.Stop();
        main = _teleportlOut.main;
        color.a = 0.1f;
        main.startColor = color;
        _teleportlOut.Play();
        _teleportlLight.color = color;
    }
    public Color SetRandomColor(Color color)
    {
        var material = _cellsMaterials.Where(m => m.color != color).ElementAt(Random.Range(0, _cellsMaterials.Count-1));
        SetMaterialTint(material.color);
        return material.color;
    }
}
