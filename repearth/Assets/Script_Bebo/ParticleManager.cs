using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticleSystem particleSys;
    private Material particleMaterial;
    private ParticleSystem.MainModule particleMain;
    [SerializeField] Color startingColor;

    public Color Color
    {
        get => particleMain.startColor.color;
        set => particleMain.startColor = value;
    }

    public Material Material
    {
        get => particleMaterial;
        set => particleMaterial = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        particleSys = GetComponent<ParticleSystem>();
        particleMaterial = GetComponent<ParticleSystemRenderer>().material;
        particleMain = particleSys.main;
        Color = startingColor;
    }
}
