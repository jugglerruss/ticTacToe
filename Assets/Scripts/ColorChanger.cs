using System.Collections;
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
        main.startColor = color;
        _teleportlIn.Play();
        _teleportlOut.Stop();
        color.a = 0.1f;
        main = _teleportlOut.main;
        main.startColor = color;
        _teleportlOut.Play();
        _teleportlLight.color = color;
    }
    public Color SetRandomColor()
    {
        var material = _cellsMaterials.ElementAt(Random.Range(0, _cellsMaterials.Count));
        SetMaterialTint(material.color);
        return material.color;
    }
}
