using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticleSystem.MainModule particleSys;

    public Color Color
    {
        get => particleSys.startColor.color;
        set
        {
            particleSys.startColor = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        particleSys = GetComponent<ParticleSystem>().main;
    }
}
